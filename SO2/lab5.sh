#!/bin/bash
# Marek Machliński 241308

if [ -z "$1" ]; then
    echo "No catalog specified!"
    exit 1
fi

rm lab5_file >/dev/null 2>&1
find "$1" -type f | while read -r path
do
    base=$(basename "$path")
    if [ "$base" == "$(echo "$base" | rev)" ]; then
        echo "$path" | tee -a lab5_file
    fi
done