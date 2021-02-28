#include "BMP.h"
#include <fstream>
#include <iostream>
#include <iomanip>

extern "C" bool checkSSE2();

uint8_t BMP::checkColorHeader()
{
    BMPColorHeader expectedColorHeader;
    if (expectedColorHeader.maskAlpha != colorHeader.maskAlpha)
        isAlphaLast = false;
    if (expectedColorHeader.colorSpaceType != colorHeader.colorSpaceType)
        return NO_SRGB_COLOR;
    return SUCCESS;
}

void BMP::writeHeaders(std::ofstream &fout) const
{
    fout.write((const char *)&fileHeader, sizeof(fileHeader));
    fout.write((const char *)&infoHeader, sizeof(infoHeader));
    if (infoHeader.bitCount == 32)
        fout.write((const char *)&colorHeader, sizeof(colorHeader));
}

void BMP::writeHeadersData(std::ofstream &fout) const
{
    writeHeaders(fout);
    fout.write((const char *)data, fileHeader.fileSize - fileHeader.offsetData);
}

uint32_t BMP::makeStrideAligned(uint32_t alignStride) const
{
    uint32_t stride = rowStride;
    while (stride % alignStride != 0)
        ++stride;
    return stride;
}

BMP::BMP(int32_t width, int32_t height, bool hasAlpha)
{
    if (width < 1 || height < 1)
        throw std::runtime_error("The image width and height must be positive numbers.");
    infoHeader.width = width;
    infoHeader.height = height;
    size = width * height;
    if (hasAlpha)
    {
        infoHeader.size = sizeof(BMPInfoHeader) + sizeof(BMPColorHeader);
        fileHeader.offsetData = sizeof(BMPFileHeader) + sizeof(BMPInfoHeader) + sizeof(BMPColorHeader);
        infoHeader.bitCount = 32;
        infoHeader.compression = 3;
        rowStride = width * 4;
        data = (uint8_t *)malloc(size);
        fileHeader.fileSize = fileHeader.offsetData + size;
    }
    else
    {
        infoHeader.size = sizeof(BMPInfoHeader);
        fileHeader.offsetData = sizeof(BMPFileHeader) + sizeof(BMPInfoHeader);
        infoHeader.bitCount = 24;
        infoHeader.compression = 0;
        rowStride = width * 3;
        data = (uint8_t *)malloc(size);
        uint32_t stride = makeStrideAligned(4);
        fileHeader.fileSize = fileHeader.offsetData + size + infoHeader.height * (stride - rowStride);
    }
}

void BMP::negativePixel(size_t pixelNumber)
{
    if (pixelNumber < size && ((isAlphaLast && pixelNumber % 4 != 3) || pixelNumber % 4 || infoHeader.bitCount == 24))
        data[pixelNumber] = ~data[pixelNumber];
}

void BMP::negativeRow(uint32_t rowNumber)
{
    if (rowNumber < infoHeader.height)
    {
        uint64_t rowSize = infoHeader.width * infoHeader.bitCount / 8;
        bool sse2Enable = checkSSE2();
        uint64_t i = rowNumber * rowSize;
        if (infoHeader.bitCount == 32)
        {
            if (isAlphaLast)
            {
                if (sse2Enable)
                {
                    uint64_t __attribute__((aligned(16))) mask[2]{0xFFFFFF00FFFFFF00, 0xFFFFFF00FFFFFF00};
                    for (uint64_t counter = i + rowSize & 0xFFFFFFFFFFFFFFF0; i < counter; i += 16)
                    {
                        __asm__("mov %0, %%rax\n"
                                "movdqa (%%rax, %1), %%xmm0\n"
                                "pxor %2, %%xmm0\n"
                                "movdqa %%xmm0, (%%rax, %1)"
                                : "=m"(data)
                                : "r"(i), "m"(mask)
                                : "%xmm0", "%rax");
                    }
                }
                for (uint64_t counter = (rowNumber + 1) * rowSize; i < counter; ++i)
                {
                    if (i % 4 != 3)
                        data[i] = ~data[i];
                }
            }
            else
            {
                if (sse2Enable)
                {
                    uint64_t __attribute__((aligned(16))) mask[2]{0xFFFFFF00FFFFFF, 0xFFFFFF00FFFFFF};
                    for (uint64_t counter = i + rowSize & 0xFFFFFFFFFFFFFFF0; i < counter; i += 16)
                    {
                        __asm__("mov %0, %%rax\n"
                                "movdqa (%%rax, %1), %%xmm0\n"
                                "pxor %2, %%xmm0\n"
                                "movdqa %%xmm0, (%%rax, %1)"
                                : "=m"(data)
                                : "r"(i), "m"(mask)
                                : "%xmm0", "%rax");
                    }
                }
                for (uint64_t counter = (rowNumber + 1) * rowSize; i < counter; ++i)
                {
                    if (i % 4)
                        data[i] = ~data[i];
                }
            }
        }
        else
        {
            if (sse2Enable)
            {  
                uint8_t offset = i & 15;
                if (offset)
                {
                    for (uint64_t counter = i + 16 - offset; i < counter; ++i)
                    {
                        if (rowNumber == 0)
                            std::cout << std::dec << rowNumber << std::hex << ' ' << i << std::endl;
                        data[i] = ~data[i];
                    }
                }
                uint64_t __attribute__((aligned(16))) mask[2]{0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF};
                for (uint64_t counter = i + (rowSize - offset) & 0xFFFFFFFFFFFFFFF0; i < counter; i += 16)
                {
                    __asm__("mov %0, %%rax\n"
                            "movdqa (%%rax, %1), %%xmm0\n"
                            "pxor %2, %%xmm0\n"
                            "movdqa %%xmm0, (%%rax, %1)"
                            : "=m"(data)
                            : "r"(i), "m"(mask)
                            : "%xmm0", "%rax");
                }
            }
            for (uint64_t counter = (rowNumber + 1) * rowSize; i < counter; ++i)
            {
                data[i] = ~data[i];
            }
        }
    }
}

void BMP::negative()
{
    bool sse2Enable = checkSSE2();
    uint64_t i = 0;
    if (infoHeader.bitCount == 32)
    {
        if (isAlphaLast)
        {
            if (sse2Enable)
            {
                uint64_t __attribute__((aligned(16))) mask[2]{0xFFFFFF00FFFFFF00, 0xFFFFFF00FFFFFF00};
                for (uint64_t counter = size & 0xFFFFFFFFFFFFFFF0; i < counter; i += 16)
                {
                    __asm__("mov %0, %%rax\n"
                            "movdqa (%%rax, %1), %%xmm0\n"
                            "pxor %2, %%xmm0\n"
                            "movdqa %%xmm0, (%%rax, %1)"
                            : "=m"(data)
                            : "r"(i), "m"(mask)
                            : "%xmm0", "%rax");
                }
            }
            for (; i < size; ++i)
            {
                if (i % 4 != 3)
                    data[i] = ~data[i];
            }
        }
        else
        {
            if (sse2Enable)
            {
                uint64_t __attribute__((aligned(16))) mask[2]{0xFFFFFF00FFFFFF, 0xFFFFFF00FFFFFF};
                for (uint64_t counter = size & 0xFFFFFFFFFFFFFFF0; i < counter; i += 16)
                {
                    __asm__("mov %0, %%rax\n"
                            "movdqa (%%rax, %1), %%xmm0\n"
                            "pxor %2, %%xmm0\n"
                            "movdqa %%xmm0, (%%rax, %1)"
                            : "=m"(data)
                            : "r"(i), "m"(mask)
                            : "%xmm0", "%rax");
                }
            }
            for (; i < size; ++i)
            {
                if (i % 4)
                    data[i] = ~data[i];
            }
        }
    }
    else
    {
        if (sse2Enable)
        {
            uint64_t __attribute__((aligned(16))) mask[2]{0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF};
            for (uint64_t counter = size & 0xFFFFFFFFFFFFFFF0; i < counter; i += 16)
            {
                __asm__("mov %0, %%rax\n"
                        "movdqa (%%rax, %1), %%xmm0\n"
                        "pxor %2, %%xmm0\n"
                        "movdqa %%xmm0, (%%rax, %1)"
                        : "=m"(data)
                        : "r"(i), "m"(mask)
                        : "%xmm0", "%rax");
            }
        }
        for (; i < size; ++i)
        {
            data[i] = ~data[i];
        }
    }
}

void BMP::set(uint8_t value)
{
    for (uint32_t i = 0; i < size; ++i)
        data[i] = value;
}

uint8_t BMP::read(const std::string &fileName)
{
    std::ifstream fin(fileName, std::ios_base::binary);
    if (fin.good())
    {
        fin.read((char *)&fileHeader, sizeof(fileHeader));
        if (fileHeader.fileType != 0x4D42)
        {
            fin.close();
            return UNKNOWN_FILE_TYPE;
        }
        fin.read((char *)&infoHeader, sizeof(infoHeader));
        // The BMPColorHeader is used only for transparent images
        if (infoHeader.bitCount == 32)
        {
            // Check if the file has bit mask color information
            if (infoHeader.size >= (sizeof(BMPInfoHeader) + sizeof(BMPColorHeader)))
            {
                fin.read((char *)&colorHeader, sizeof(colorHeader));
                uint8_t colorCheck = checkColorHeader();
                if (colorCheck != SUCCESS)
                {
                    fin.close();
                    return colorCheck;
                }
            }
            else
            {
                fin.close();
                return NO_BIT_MASK;
            }
        }
        // Jump to the pixel data location
        fin.seekg(fileHeader.offsetData, fin.beg);
        // Adjust the header fields for output.
        // Some editors will put extra info in the image file, we only save the headers and the data.
        if (infoHeader.bitCount == 32)
        {
            infoHeader.size = sizeof(BMPInfoHeader) + sizeof(BMPColorHeader);
            fileHeader.offsetData = sizeof(BMPFileHeader) + sizeof(BMPInfoHeader) + sizeof(BMPColorHeader);
        }
        else
        {
            infoHeader.size = sizeof(BMPInfoHeader);
            fileHeader.offsetData = sizeof(BMPFileHeader) + sizeof(BMPInfoHeader);
        }
        fileHeader.fileSize = fileHeader.offsetData;
        if (infoHeader.height < 0)
        {
            fin.close();
            return NO_ORIGIN_IN_LEFT_CORNER;
        }
        size = infoHeader.width * infoHeader.height * infoHeader.bitCount / 8;
        if (data != nullptr)
            free(data);
        data = (uint8_t *)malloc(size);
        // Here we check if we need to take into account row padding
        if (infoHeader.width % 4 == 0)
        {
            fin.read((char *)data, size);
            fileHeader.fileSize += size;
        }
        else
        {
            rowStride = infoHeader.width * infoHeader.bitCount / 8;
            uint32_t stride = makeStrideAligned(4);
            std::vector<uint8_t> padding_row(stride - rowStride);
            for (int y = 0; y < infoHeader.height; ++y)
            {
                fin.read((char *)(data + rowStride * y), rowStride);
                fin.read((char *)padding_row.data(), padding_row.size());
            }
            fileHeader.fileSize += size + infoHeader.height * padding_row.size();
        }
        fin.close();
        return SUCCESS;
    }
    else
    {
        fin.close();
        return CANNOT_OPEN_FILE;
    }
}

uint8_t BMP::write(const std::string &fileName, bool product) const
{
    std::ofstream fout(product ? "NEG_" + fileName : fileName, std::ios_base::binary | std::ios_base::trunc);
    if (fout.good())
    {
        if (infoHeader.bitCount == 32 || infoHeader.compression == 3)
            writeHeadersData(fout);
        else if (infoHeader.bitCount == 24 || infoHeader.compression == 0)
        {
            if (infoHeader.width % 4 == 0)
                writeHeadersData(fout);
            else
            {
                uint32_t stride = makeStrideAligned(4);
                std::vector<uint8_t> padding_row(stride - rowStride);
                writeHeaders(fout);
                for (int y = 0; y < infoHeader.height; ++y)
                {
                    fout.write((const char *)(data + rowStride * y), rowStride);
                    fout.write((const char *)padding_row.data(), padding_row.size());
                }
            }
        }
        else
        {
            fout.close();
            return WRONG_PIXEL_SIZE;
        }
        fout.close();
        return SUCCESS;
    }
    else
    {
        fout.close();
        return CANNOT_OPEN_FILE;
    }
}

uint8_t BMP::readRow(const std::string &fileName, uint32_t rowNumber)
{
    std::ifstream fin("_row_" + std::to_string(rowNumber) + "_NEG_" + fileName, std::ios_base::binary);
    if (fin.good())
    {
        size_t rowSize = infoHeader.width * infoHeader.bitCount / 8;
        fin.read((char *)(data + rowNumber * rowSize), rowSize);
        fin.close();
        remove(("_row_" + std::to_string(rowNumber) + "_NEG_" + fileName).c_str());
        return SUCCESS;
    }
    else
    {
        fin.close();
        return CANNOT_OPEN_FILE;
    }
}

uint8_t BMP::writeRow(const std::string &fileName, uint32_t rowNumber) const
{
    std::ofstream fout("_row_" + std::to_string(rowNumber) + "_NEG_" + fileName, std::ios_base::binary);
    if (fout.good())
    {
        size_t rowSize = infoHeader.width * infoHeader.bitCount / 8;
        fout.write((const char *)(data + rowNumber * rowSize), rowSize);
        fout.close();
        return SUCCESS;
    }
    else
    {
        fout.close();
        return CANNOT_OPEN_FILE;
    }
}