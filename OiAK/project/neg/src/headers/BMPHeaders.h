#ifndef BMPHeaders_H
#define BMPHeaders_H
#include <stdint.h>

constexpr int8_t SUCCESS = 0;
constexpr int8_t CANNOT_OPEN_FILE = -1;
constexpr int8_t UNKNOWN_FILE_TYPE = -2;
constexpr int8_t NO_BIT_MASK = -3;
constexpr int8_t NO_BGRA_MASK = -4;
constexpr int8_t NO_SRGB_COLOR = -5;
constexpr int8_t NO_ORIGIN_IN_LEFT_CORNER = -6;
constexpr int8_t WRONG_PIXEL_SIZE = -7;
constexpr int8_t CANNOT_CREATE_MEMORY = -8;
constexpr int8_t CANNOT_OPEN_MEMORY = -9;
constexpr int8_t CANNOT_RESIZE_MEMORY = -10;
constexpr int8_t CANNOT_CLOSE_MEMORY = -11;
constexpr int8_t NO_INPUT_PARAMS = -12;
constexpr int8_t CANNOT_CREATE_CHILD = -13;

#pragma pack(push, 1)
struct BMPFileHeader
{
    uint16_t fileType = 0x4D42;   // File type always BM which is 0x4D42
    uint32_t fileSize = 0;   // Size of the file (in bytes)
    uint16_t reserved1 = 0;  // Reserved, always 0
    uint16_t reserved2 = 0;  // Reserved, always 0
    uint32_t offsetData = 54; // Start position of pixel data (bytes from the beginning of the file)
};

struct BMPInfoHeader
{
    uint32_t size = 40;       // Size of this header (in bytes)
    int32_t width = 0;        // width of bitmap in pixels
    int32_t height = 0;       // width of bitmap in pixels
                              //       (if positive, bottom-up, with origin in lower left corner)
                              //       (if negative, top-down, with origin in upper left corner)
    uint16_t planes = 1;      // No. of planes for the target device, this is always 1
    uint16_t bitCount = 0;    // No. of bits per pixel
    uint32_t compression = 0; // 0 or 3 - uncompressed. THIS PROGRAM CONSIDERS ONLY UNCOMPRESSED BMP images
    uint32_t imageSize = 0;   // 0 - for uncompressed images
    int32_t pixelsPerMeterX = 0;
    int32_t pixelsPerMeterY = 0;
    uint32_t colorsUsed = 0;      // No. color indexes in the color table. Use 0 for the max number of colors allowed by bitCount
    uint32_t colorsImportant = 0; // No. of colors used for displaying the bitmap. If 0 all colors are required
};

struct BMPColorHeader
{
    uint32_t maskRed = 0x00ff0000;        // Bit mask for the red channel
    uint32_t maskGreen = 0x0000ff00;      // Bit mask for the green channel
    uint32_t maskBlue = 0x000000ff;       // Bit mask for the blue channel
    uint32_t maskAlpha = 0xff000000;      // Bit mask for the alpha channel
    uint32_t colorSpaceType = 0x73524742; // Default "sRGB" (0x73524742)
    uint32_t unused[16]{0};               // Unused data for sRGB color space
};
#pragma pack(pop)

#endif