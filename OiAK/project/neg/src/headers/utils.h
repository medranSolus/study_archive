#ifndef UTILS_H
#define UTILS_H
#include "BMP.h"
#include <string>
#include <cstring>
#include <unistd.h>
#include <fcntl.h>
#include <sys/mman.h>
#include <sys/stat.h>

void quit(int code, BMP * image);
int createSharedMemory(const char *name);
void *openSharedMemory(size_t size, int id);
BMP * createSharedImage(BMP * image, const std::string & name);
BMP * openSharedImage(const std::string & name);
uint8_t closeSharedImage(BMP * sharedImage);
void destroySharedMemory(const std::string &name);

#endif