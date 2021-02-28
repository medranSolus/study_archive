#include "pch.h"
#include <fstream>
#include "List.h"
namespace DataTypes
{
	List::List(const std::string & fileName)
	{
		std::ifstream fin(fileName);
		if (fin.good())
		{
			fin >> count;
			if (count)
			{
				head = new Node;
				fin >> *head;
				tail = head;
				for (long i = 1; i < count; ++i)
				{
					*tail->next() = new Node;
					fin >> **tail->next();
					tail = *tail->next();
				}
			}
		}
		fin.close();
	}

	bool List::contains(long value) const
	{
		if (head != nullptr)
		{
			Node * temp = head;
			do
			{
				if (temp->getData() == value)
					return true;
				else if (*temp->next() == nullptr)
					return false;
				else
					temp = *temp->next();
			} while (true);
		}
		return false;
	}

	void List::popFront()
	{
		if (head != nullptr)
		{
			Node * newHead = *head->next();
			delete head;
			head = newHead;
			if (head == nullptr)
				tail = nullptr;
			--count;
		}
	}

	void List::popBack()
	{
		if (head != nullptr)
		{
			if (*head->next() != nullptr)
			{
				Node * prev = head;
				while (*prev->next() != tail)
					prev = *prev->next();
				delete tail;
				tail = prev;
				*tail->next() = nullptr;
			}
			else
			{
				delete head;
				tail = head = nullptr;
			}
			--count;
		}
	}

	void List::erase(long value)
	{
		if (head != nullptr)
		{
			if (head->getData() == value)
				popFront();
			else if (tail->getData() == value)
				popBack();
			else
			{
				Node * temp = head;
				while (*temp->next() != tail)
				{
					if ((*temp->next())->getData() == value)
					{
						Node * toDelete = *temp->next();
						*temp->next() = *(*temp->next())->next();
						delete toDelete;
						--count;
						return;
					}
					temp = *temp->next();
				}
			}
		}
	}

	void List::pushFront(long value)
	{
		Node * newHead = new Node(value, head);
		if (head == nullptr)
			tail = newHead;
		head = newHead;
		++count;
	}

	void List::pushBack(long value)
	{
		if (tail == nullptr)
			tail = head = new Node(value);
		else
			tail = *tail->next() = new Node(value);
		++count;
	}

	void List::insert(long afterValue, long value)
	{
		if (head != nullptr)
		{
			if (head->getData() == afterValue)
			{
				*head->next() = new Node(value, *head->next());
				++count;
			}
			else if (tail->getData() == afterValue)
			{
				tail = *tail->next() = new Node(value);
				++count;
			}
			else if (*head->next() != nullptr)
			{
				Node * temp = *head->next();
				while (temp != tail)
				{
					if (temp->getData() == afterValue)
					{
						*temp->next() = new Node(value, *temp->next());
						++count;
						return;
					}
					temp = *temp->next();
				}
			}
		}
		pushFront(value);
	}

	void List::clear()
	{
		if (head != nullptr)
		{
			Node * next = *head->next();
			if (next != nullptr)
			{
				do
				{
					delete head;
					head = next;
					next = *next->next();
				} while (next != nullptr);
			}
			delete head;
			head = tail = nullptr;
			count = 0;
		}
	}

	std::ostream & operator<<(std::ostream & os, const List & list)
	{
		if (list.head != nullptr)
		{
			List::Node * temp = list.head;
			while (temp != list.tail)
			{
				os << temp->getData() << "->";
				temp = *temp->next();
			}
			os << list.tail->getData() << std::endl;
		}
		return os;
	}

	std::ofstream & operator<<(std::ofstream & of, const List & list)
	{
		of << list.count;
		List::Node * temp = list.head;
		while (temp != nullptr)
		{
			of << std::endl << temp->getData();
			temp = *temp->next();
		}
		return of;
	}

	std::istream & operator>>(std::istream & is, List & list)
	{
		list.clear();
		is >> list.count;
		for (long i = 0, temp; i < list.count; ++i)
		{
			is >> temp;
			list.pushBack(temp);
		}
		return is;
	}

	List::~List()
	{
		if (head != nullptr)
		{
			Node * next = *head->next();
			if (next != nullptr)
			{
				do
				{
					delete head;
					head = next;
					next = *next->next();
				} while (next != nullptr);
			}
			delete head;
		}
	}
}