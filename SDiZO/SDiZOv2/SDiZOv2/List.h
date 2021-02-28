#pragma once


class List
{
	class Node
	{
		Node * next = nullptr;
		long long data = MINLONGLONG;
	public:
		Node() {}
		Node(long long x, Node * nextNode = nullptr);

		inline Node * getNext() const { return next; }
		inline long long getData() const { return data; }

		inline void setNext(Node * nextNode) { next = nextNode; }
		inline void setData(long long x) { data = x; }
	};
	Node * head = nullptr;
	Node * tail = nullptr;

public:
	List() {}

	inline bool empty() const { return !head; }

	long long front();

	void pushBack(long long x);
	void pushFront(long long x);
	void pop();

	~List();
};