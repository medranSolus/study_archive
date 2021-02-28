#!/bin/bash
# Marek Machliński 241308

src_dir="$PWD/$1"
out_dir="$PWD/$2"

if [ -z "$src_dir" ]; then
    echo "No source catalog specified!"
    exit 1
fi
if ! [[ -r "$src_dir" ]]; then
    echo "Cannot read catalog: $src_dir"
    exit 2
fi
if [ -z "$out_dir" ]; then
    echo "Must specify destination catalog!"
    exit 3
fi
if ! [[ -w "$out_dir" ]]; then
    echo "Cannot write to catalog: $out_dir"
    exit 4
fi

for dir in $(find "$src_dir" -mindepth 1 -type d)
do
    for file in $(find "$dir" -executable -type f)
    do
        ln -s -f -T "$file" "$out_dir/$(basename $file)"
    done;
done;

for file in $(find "$src_dir" -maxdepth 1 -executable -type f)
do
    outfile="$out_dir/$(basename $file)"
    if ! [ "$file" -ef "$outfile" ]; then
        ln -f -T "$file" "$outfile"
    fi
done;