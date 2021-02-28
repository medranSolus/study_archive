#ifndef BMP_H
#define BMP_H
#include "BMPHeaders.h"
#include <vector>
#include <string>

class BMP
{
    BMPFileHeader fileHeader;
    BMPInfoHeader infoHeader;
    BMPColorHeader colorHeader;
    #pragma pack(push, 16)
    uint8_t * __attribute__((aligned(16))) data = nullptr;
    #pragma pack(pop)
    uint64_t size = 0;
    uint32_t rowStride = 0;
    bool isAlphaLast = true;

    uint8_t checkColorHeader();
    void writeHeaders(std::ofstream &fout) const;
    void writeHeadersData(std::ofstream &fout) const;
    // Add 1 to the rowStride until it is divisible with alignStride
    uint32_t makeStrideAligned(uint32_t alignStride) const;

public:
    BMP() {}
    inline BMP(const std::string &fileName) { read(fileName); }
    BMP(int32_t width, int32_t height, bool hasAlpha = true);

    inline int32_t rows() const { return infoHeader.height; }
    inline int32_t columns() const { return infoHeader.width; }
    inline uint64_t rowSize() const { return size / infoHeader.height; }
    inline uint64_t dataSize() const { return size; }
    inline uint8_t * getData() { return data; }
    inline void setData(uint8_t * newData) { data = newData; }
    inline void clear() { free(data); }

    void negativePixel(size_t pixelNumber);
    void negativeRow(uint32_t rowNumber);
    void negative();
    void set(uint8_t value);

    uint8_t read(const std::string &fileName);
    uint8_t write(const std::string &fileName, bool product = true) const;
    uint8_t readRow(const std::string &fileName, uint32_t rowNumber);
    uint8_t writeRow(const std::string &fileName, uint32_t rowNumber) const;
};
#endif