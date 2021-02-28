#pragma once
#include "IAlgorithms.h"
#include "MinimalSpanTree.h"

class GraphList : public IAlgorithms
{
	long long vertexes = 0;
	long long edges = 0;
	Edge ** list = nullptr;

	Edge * getEdge(long long first, long long last) const;
public:
	GraphList(std::ifstream & fin);
	GraphList(long long vertexCount, long long edgeCount);

	void set(long long vertexCount, long long edgeCount);

	void show() const;
	MinimalSpanTree * minimalTreePrim() const;
	MinimalSpanTree * minimalTreeKruskal() const;
	Path * findPathDijkstra(long long first, long long last) const;
	Path * findPathFordBellman(long long first, long long last) const;
	FlowList * maximalFlowBFS(long long source, long long target) const;
	FlowList * maximalFlowDFS(long long source, long long target) const;

	~GraphList();
};

