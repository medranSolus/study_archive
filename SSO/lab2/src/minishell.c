#define _POSIX_C_SOURCE 200809L
#define _GNU_SOURCE
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/wait.h>
#include <fcntl.h>
#include <errno.h>
#include <unistd.h>

#define NONBLOCK_COUNT 8

char*** parse_arguments(int argc, char** argv, int* commands_count);
void run(int in_desc, int out_desc, char** args, bool setInput);

int main(int argc, char** argv)
{
    if (argc > 2)
    {
        int commands_count = 0;
        char*** commands = parse_arguments(argc, argv, &commands_count);
        
        int** pipe_descs = (int**)calloc(commands_count, sizeof(int*));
        for (int i = 0; i < commands_count - 1; ++i)
        {
            pipe_descs[i] = (int*)calloc(2, sizeof(int));
            if (pipe2(pipe_descs[i], O_CLOEXEC) == -1)
            {
                perror("pipe");
                return -1;
            }
        }
        const int last_pipe = commands_count - 1;
        pipe_descs[last_pipe] = (int*)calloc(2, sizeof(int));
        if (pipe2(pipe_descs[last_pipe], O_NONBLOCK) == -1)
        {
            perror("pipe2");
            return -2;
        }
        run(0, pipe_descs[0][1], commands[0], false);
        for (int i = 1; i < commands_count; ++i)
        {
            run(pipe_descs[i - 1][0], pipe_descs[i][1], commands[i], true);
            close(pipe_descs[i - 1][0]);
            close(pipe_descs[i - 1][1]);
        }
        int retry = NONBLOCK_COUNT;
        while (true)
        {
            char c;
            int status = read(pipe_descs[last_pipe][0], &c, sizeof(c));
            if (status == -1)
            {
                if (errno == EWOULDBLOCK || errno == EAGAIN)
                {
                    sleep(1);
                    --retry;
                    if (retry)
                        continue;
                    else
                        break;
                }
                perror("read");
                return -3;
            }
            printf("%c", toupper(c));
            retry = NONBLOCK_COUNT;
        }
        for (int i = 0; i < commands_count; ++i)
            wait(NULL);
        for (int i = 0; i < commands_count; ++i)
        {
            free(pipe_descs[i]);
            free(commands[i]);
        }
        free(pipe_descs);
        free(commands);
    }
    return 0;
}

void run(int in_desc, int out_desc, char** args, bool setInput)
{
    pid_t id = fork();
    if (id == 0)
    {
        if (setInput)
        {
            if (dup2(in_desc, STDIN_FILENO) == -1)
            {
                perror("stdin dup2");
                exit(-4);
            }
        }
        if (dup2(out_desc, STDOUT_FILENO) == -1)
        {
            perror("stdout dup2");
            exit(-5);
        }
        if (dup2(out_desc, STDERR_FILENO) == -1)
        {
            perror("stderr dup2");
            exit(-6);
        }
        int size = strlen(args[0]);
        char* path = (char*)calloc(6 + size, sizeof(char));
        path[0] = '/';
        path[1] = 'b';
        path[2] = 'i';
        path[3] = 'n';
        path[4] = '/';
        for (int i = 0; i < size; ++i)
            path[i + 5] = args[0][i];
        path[5 + size] = '\0';
        execv(path, args);
        perror("execv");
        exit(-7);
    }
    else if (id == -1)
    {
        perror("fork");
        exit(-8);
    }
}

char*** parse_arguments(int argc, char** argv, int* commands_count)
{
    char*** commands = NULL;
    const char* delimeter = ".";
    int command_begin = 1;
    for (int i = 1; i < argc; ++i)
    {
        if (strcmp(argv[i], delimeter) == 0)
        {
            int arg_count = i - command_begin;
            if (arg_count > 0)
            {
                const int current_command = *commands_count;
                commands = (char***)realloc(commands, ++(*commands_count) * sizeof(char***));
                commands[current_command] = (char**)calloc(arg_count + 1, sizeof(char**));
                for (int j = 0; j < arg_count; ++j)
                    commands[current_command][j] = argv[command_begin + j];
                commands[current_command][arg_count] = NULL;
            }
            command_begin = i + 1;
        }
    }
    return commands;
}