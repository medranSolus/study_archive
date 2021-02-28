#include "utils.h"
#include <cstdlib>

int main(int argc, char **argv)
{
    if (argc < 2)
        return NO_INPUT_PARAMS;
    uint32_t row = atoi(argv[1]);
    uint32_t count = 1;
    if (argc == 3)
        count = atoi(argv[2]);
    BMP *image = openSharedImage(argv[0]);
    for(uint32_t i = 0; i < count; ++i)
        image->negativeRow(i + row);
    if (closeSharedImage(image))
        return CANNOT_CLOSE_MEMORY;
    return SUCCESS;
}