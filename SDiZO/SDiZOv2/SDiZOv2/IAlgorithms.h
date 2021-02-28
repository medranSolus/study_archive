#pragma once
#include "MinimalSpanTree.h"
#include <fstream>

enum Connection { None, First, Last};

__interface IAlgorithms
{
	void set(long long vertexCount, long long edgeCount);

	void show() const;
	MinimalSpanTree * minimalTreePrim() const;
	MinimalSpanTree * minimalTreeKruskal() const;
	Path * findPathDijkstra(long long first, long long last) const;
	Path * findPathFordBellman(long long first, long long last) const;
};