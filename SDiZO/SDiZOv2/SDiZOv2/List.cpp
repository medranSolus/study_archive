#include "pch.h"
#include "List.h"

List::Node::Node(long long x, Node * nextNode) : data(x), next(nextNode) {}

long long List::front()
{
	if (head) 
		return head->getData();
	else     
		return MINLONGLONG;
}

void List::pushBack(long long x)
{
	Node * temp = new Node(x);
	if (tail) 
		tail->setNext(temp);
	else     
		head = temp;
	tail = temp;
}

void List::pushFront(long long x)
{
	head = new Node(x, head);
	if (!tail)
		tail = head;
}

void List::pop(void)
{
	if (head)
	{
		Node * temp = head;
		head = head->getNext();
		if (!head) 
			tail = nullptr;
		delete temp;
	}
}

List::~List()
{
	Node * temp = nullptr;
	while (head)
	{
		temp = head->getNext();
		delete head;
		head = temp;
	}
}
