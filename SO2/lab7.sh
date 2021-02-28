#!/bin/bash
# Marek Machliński 241308

server_path=http://datko.pl/SO2/
file=kant.txt

if ! [ -f "$file" ]; then
    wget "$server_path$file"
fi

awk 'BEGIN { RS="[<>]" } /@/' "$file"