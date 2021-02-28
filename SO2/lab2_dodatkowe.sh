#!/bin/bash
# Marek Machliński 241308

if [ -z "$1" ]; then
    echo "No catalog specified!"
    exit -1
fi
ls $1 >/dev/null 2>&1 ||
{
    echo "$1 is not a directory!"
    exit -2
}
if [ -z "$2" ]; then
    echo "Need to specify second catalog!"
    exit -3
fi
ls $2 >/dev/null 2>&1 ||
{
    echo "$2 is not a directory!"
    exit -4
}

files_1=$(ls -SFd $(find $1) | grep '\*')
files_1=$(echo "${files_1//\*}" | cut -d'/' -f2-)
files_2=$(ls -SFd $(find $2) | grep '\*')
files_2=$(echo "${files_2//\*}" | cut -d'/' -f2-)
result=
for file1 in $files_1
do
    for file2 in $files_2
    do
        if [ "$file1" == "$file2" ]; then
            result="$file2 $result"
        fi
    done
done
echo ${result}