#!/bin/bash

filename=""
problem_num=$1
template_filename=$2


function show_usage() {
	echo "$Usage: $0 <problem_number> <template_file>"
}

function append_template() {
	content="$(cat $template_filename)"
	# `echo -e ${content} >> $filename`
	`echo -e "${content//ProblemNum/$problem_num}" >> $filename`
}

if [ $# -eq 2 ]; then
	filename="Baekjoon$problem_num.cs"
	touch $filename
	append_template
else
	show_usage
fi

