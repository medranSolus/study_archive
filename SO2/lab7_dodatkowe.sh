#!/bin/bash
# Marek Machliński 241308

server_path=http://datko.pl/SO2/
file_a=lipsum.txt
file_b=core_filter.py
file_c=tox.ini

if ! [ -f "$file_a" ]; then
    wget "$server_path$file_a"
fi
if ! [ -f "$file_b" ]; then
    wget "$server_path$file_b"
fi
if ! [ -f "$file_c" ]; then
    wget "$server_path$file_c"
fi

#a)
awk 'BEGIN { FS = ""; RS = " " } $NF == $1' "$file_a"

#b)
awk '/class ([A-Z])([a-z])*/ \
    { \
        print
    }' "$file_b"

#c)
awk 'BEGIN { FS = " = " } \
    { \
        if ($0 ~ / = / && $2 != RS) \
        { \
            printf $1 " =\n"; \
            printf " " $2 "\n"; \
        } \
        else \
            print $0; \
    }' "$file_c" | tr -s " "