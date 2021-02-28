#!/bin/bash
# Marek Machliński 241308

if [ -z "$1" ]; then
    echo "No catalog specified!"
    exit -1
fi
if ! [[ -r "$1" ]]; then
    echo "Cannot read catalog: $1"
    exit -2
fi

links=$(find "$1" -maxdepth 1 -type l)
socks=$(find "$1" -maxdepth 1 -type s)
dirs=$(find "$1" -maxdepth 1 -type d)
files=$(find "$1" -maxdepth 1 -type f)
echo "Number of files, dirs, symlinks and sockets inside '$1': $(wc -w <<< "$links $socks $dirs $files")"

if ! [ -z "$2" ]; then
    belongs_to_user=0
    for link in $links
    do
        if [ "$(stat --format="%U" "$link")" == "$2" ]; then
            ((belongs_to_user++))
        fi
    done;
    echo "Number of symlinks belonging to $2: $belongs_to_user"
    belongs_to_user=0
    for sock in $socks
    do
        if [ "$(stat --format="%U" "$sock")" == "$2" ]; then
            ((belongs_to_user++))
        fi
    done;
    echo "Number of sockets belonging to $2: $belongs_to_user"
    belongs_to_user=0
    for dir in $dirs
    do
        if [ "$(stat --format="%U" "$dir")" == "$2" ]; then
            ((belongs_to_user++))
        fi
    done;
    echo "Number of dirs belonging to $2: $belongs_to_user"
    belongs_to_user=0
    for file in $files
    do
        if [ "$(stat --format="%U" "$file")" == "$2" ]; then
            ((belongs_to_user++))
        fi
    done;
    echo "Number of files belonging to $2: $belongs_to_user"
fi

small_files=0
medium_files=0
big_files=0
for file in $files
do
    size="$(stat --format="%U" "$file")"
    if ((size < 0x100000)); then
        ((small_files++))
    elif ((size > 0x6400000)); then
        ((big_files++))
    else
        ((medium_files++))
    fi
done;
echo "Number of files smaller than 1 MiB: $small_files"
echo "Number of files bigger than 100 MiB: $big_files"
echo "Number of files with size in <1 MiB, 100 MiB>: $medium_files"

for link in $links
do
    if [[ -L "$link" ]] && [[ ! -a "$link" ]];then 
        echo "Broken symlink: $link"
    fi
done;