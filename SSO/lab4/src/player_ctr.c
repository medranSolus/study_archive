#define _POSIX_C_SOURCE 200809L
#include <stdbool.h>
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h> 
#include <fcntl.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/wait.h>
#include <termios.h>

static const char* fifo = "./bin/player_ctr.fif";

bool decode_command(int fd, char command);
void init_termios(struct termios* old);
static inline void reset_termios(const struct termios* old) { tcsetattr(0, TCSANOW, old); }

int main(int argc, char** argv)
{
    if (argc == 1)
    {
        printf("Specify file to play!");
        return -1;
    }
    if (mkfifo(fifo, 0777) == -1)
    {
        perror("mkfifo");
        return -2;
    }
    const int pid = fork();
    if (pid == 0)
    {
        static const char* file = "file=\"";
        int fifo_size = strlen(fifo);
        int file_size = strlen(file);
        char* input_file = (char*)calloc(fifo_size + file_size + 2, sizeof(char));
        int i = 0;
        for (; i < file_size; ++i)
            input_file[i] = file[i];
        for (int j = 0; j < fifo_size; ++i, ++j)
            input_file[i] = fifo[j];
        input_file[i] = '\"';
        input_file[i + 1] = '\0';
        execl("/bin/mplayer", "mplayer", "-really-quiet", "-input", input_file, argv[1], NULL);
        perror("execl");
        exit(-3);
    }
    else if (pid == -1)
    {
        perror("fork");
        unlink(fifo);
        return -4;
    }
    const int fd = open(fifo, O_RDWR);
    if (fd == -1)
    {
        perror("open");
        unlink(fifo);
        return -5;
    }
    printf("Player control:\n\
        [P] Play\n\
        [p] Pause\n\
        [q] Quit\n\
        [s] Stop\n\
        [u] Volume up\n\
        [d] Volume down\n\
        [f] Forward 5 seconds\n\
        [b] Backward 5 seconds\n");
    struct termios old_settings;
    init_termios(&old_settings);
    while(true)
    {
        if (decode_command(fd, getchar()))
            break;
    }
    reset_termios(&old_settings);
    close(fd);
    wait(NULL);
    unlink(fifo);
    return 0;
}

void init_termios(struct termios* old)
{
    tcgetattr(0, old);
    struct termios current = *old;
    current.c_lflag &= ~ICANON; // Disable buffered mode
    current.c_lflag |= ECHO; // Set echo mode
    current.c_cc[VMIN] = 0;
    current.c_cc[VTIME] = 0;
    tcsetattr(0, TCSANOW, &current);
}

bool decode_command(int fd, char command)
{
    switch (command)
    {
    case 'P':
    {
        static const char* play = "play\n";
        if (write(fd, play, 6) == -1)
        {
            perror("write play");
            return true;
        }
        break;
    }
    case 'p':
    {
        static const char* pause = "pause\n";
        if (write(fd, pause, 7) == -1)
        {
            perror("write pause");
            return true;
        }
        break;
    }
    case 'q':
    {
        static const char* quit = "quit\n";
        if (write(fd, quit, 6) == -1)
            perror("write quit");
        return true;
    }
    case 's':
    {
        static const char* stop = "stop\n";
        if (write(fd, stop, 6) == -1)
        {
            perror("write stop");
            return true;
        }
        break;
    }
    case 'u':
    {
        static const char* volume_up = "volume +10\n";
        if (write(fd, volume_up, 12) == -1)
        {
            perror("write volume up");
            return true;
        }
        break;
    }
    case 'd':
    {
        static const char* volume_down = "volume -10\n";
        if (write(fd, volume_down, 12) == -1)
        {
            perror("write volume down");
            return true;
        }
        break;
    }
    case 'f':
    {
        static const char* forward = "seek +5.0\n";
        if (write(fd, forward, 11) == -1)
        {
            perror("write forward");
            return true;
        }
        break;
    }
    case 'b':
    {
        static const char* backward = "seek -5.0\n";
        if (write(fd, backward, 11) == -1)
        {
            perror("write backward");
            return true;
        }
        break;
    }
    }
    return false;
}