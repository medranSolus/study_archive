#!/bin/bash
# Marek Machliński 241308
# NOTE: Enumeracja od największych do najmniejszych

if [ -z "$1" ]; then
    echo "No catalog specified!"
    exit -1
fi

files=$(ls -SF $1 | grep -Ev '/|@|\*|=|>|\|')
i=1
for file in $files
do
    mv $1/$file $1/$file.$i
    ((i++))
done