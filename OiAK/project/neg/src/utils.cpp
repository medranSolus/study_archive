#include "utils.h"

void quit(int code, BMP *image)
{
    free(image);
    exit(code);
}

int createSharedMemory(const char *name)
{
    return shm_open(name, O_RDWR | O_CREAT, S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH);
}

void *openSharedMemory(size_t size, int id)
{
    return mmap(NULL, size, PROT_READ | PROT_WRITE | PROT_EXEC, MAP_SHARED | MAP_POPULATE, id, 0);
}

BMP *createSharedImage(BMP *image, const std::string &name)
{
    int idClass = createSharedMemory(("negativeImage_class_" + name).c_str());
    int idData = createSharedMemory(("negativeImage_data_" + name).c_str());
    if (idClass == -1 || idData == -1)
        quit(CANNOT_CREATE_MEMORY, image);
    if (ftruncate(idClass, sizeof(BMP)) == -1)
        quit(CANNOT_RESIZE_MEMORY, image);
    BMP *sharedImage = (BMP *)openSharedMemory(sizeof(BMP), idClass);
    if (sharedImage == MAP_FAILED)
        quit(CANNOT_OPEN_MEMORY, image);
    memcpy(sharedImage, image, sizeof(BMP));
    if (ftruncate(idData, image->dataSize()) == -1)
        quit(CANNOT_RESIZE_MEMORY, image);
    uint8_t *data = (uint8_t *)openSharedMemory(image->dataSize(), idData);
    if (data == MAP_FAILED)
        quit(CANNOT_OPEN_MEMORY, image);
    madvise(data, image->dataSize(), MADV_HUGEPAGE);
    memcpy(data, image->getData(), image->dataSize());
    sharedImage->setData(data);
    close(idClass);
    close(idData);
    return sharedImage;
}

BMP *openSharedImage(const std::string &name)
{
    int idClass = createSharedMemory(("negativeImage_class_" + name).c_str());
    int idData = createSharedMemory(("negativeImage_data_" + name).c_str());
    if (idClass == -1 || idData == -1)
        exit(CANNOT_CREATE_MEMORY);
    BMP *sharedImage = (BMP *)openSharedMemory(sizeof(BMP), idClass);
    if (sharedImage == MAP_FAILED)
        exit(CANNOT_OPEN_MEMORY);
    uint8_t *data = (uint8_t *)openSharedMemory(sharedImage->dataSize(), idData);
    if (data == MAP_FAILED)
        exit(CANNOT_OPEN_MEMORY);
    sharedImage->setData(data);
    close(idClass);
    close(idData);
    return sharedImage;
}

uint8_t closeSharedImage(BMP *sharedImage)
{
    if (munmap(sharedImage->getData(), sharedImage->dataSize()) == -1)
        return CANNOT_CLOSE_MEMORY;
    if (munmap(sharedImage, sizeof(BMP)) == -1)
        return CANNOT_CLOSE_MEMORY;
    return SUCCESS;
}

void destroySharedMemory(const std::string &name)
{
    shm_unlink(("negativeImage_class_" + name).c_str());
    shm_unlink(("negativeImage_data_" + name).c_str());
}
