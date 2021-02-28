#pragma once
#include "IAlgorithms.h"
#include "MinimalSpanTree.h"

class GraphMatrix : public IAlgorithms
{
	Connection ** matrix = nullptr;
	long long * values = nullptr;
	long long vertexes = 0;
	long long edges = 0;

	long long getEdge(long long vertex, Connection type, long long startEdge = -1) const;
	long long getVertex(long long edge, Connection type) const;
public:
	GraphMatrix(std::ifstream & fin);
	GraphMatrix(long long vertexCount, long long edgeCount);

	inline long long vertexCount() const { return vertexes; }

	void set(long long vertexCount, long long edgeCount);

	void show() const;
	void save(std::ofstream & fout) const;

	MinimalSpanTree * minimalTreePrim() const;
	MinimalSpanTree * minimalTreeKruskal() const;
	Path * findPathDijkstra(long long first, long long last) const;
	Path * findPathFordBellman(long long first, long long last) const;
	FlowMatrixes * maximalFlowBFS(long long source, long long target) const;
	FlowMatrixes * maximalFlowDFS(long long source, long long target) const;

	~GraphMatrix();
};

