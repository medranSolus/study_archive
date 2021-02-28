#pragma once
#include "Edge.h"
#include "Node.h"

class MinimalSpanTree
{
	Node ** list = nullptr;
	long long count = 0;
	long long weight = 0;
public:
	MinimalSpanTree(long long size);

	Node * getNeighbours(long long vertex);

	void addEdge(Edge edge);
	void show();

	~MinimalSpanTree();
};