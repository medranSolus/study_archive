#include "pch.h"
#include <fstream>
#include "Heap.h"
namespace DataTypes
{
	bool Heap::searchNode(const long & value, long index) const
	{
		if (data[index] < value)
			return false;
		if (data[index] == value)
			return true;
		if (2 * index + 1 < count)
			if (searchNode(value, 2 * index + 1))
				return true;
		if (2 * index + 2 < count)
			return searchNode(value, 2 * index + 2);
		return false;
	}

	void Heap::sortUp(long index)
	{
		if (index == 0)
			return;
		long parent = (index - 1) / 2;
		if (data[parent] < data[index])
		{
			long temp = data[index];
			data[index] = data[parent];
			data[parent] = temp;
			sortUp(parent);
		}
	}

	void Heap::sortDown(long index)
	{
		long child = 2 * index + 2;
		if (child < count)
		{
			if (data[index] < data[child] && data[child] >= data[child - 1])
			{
				long temp = data[index];
				data[index] = data[child];
				data[child] = temp;
				return sortDown(child);
			}
			else if (data[index] < data[child - 1] && data[child - 1] >= data[child])
			{
				long temp = data[index];
				data[index] = data[--child];
				data[child] = temp;
				return sortDown(child);
			}
		}
		else if (--child < count)
		{
			if (data[child] > data[index])
			{
				long temp = data[index];
				data[index] = data[child];
				data[child] = temp;
			}
		}
	}

	void Heap::printHeap(long index, unsigned long depth, std::string & levelsTrace) const
	{
		if (index < count)
		{
			std::cout << '\t';
			for (unsigned long i = 0; i < depth; ++i)
			{
				if (i == depth - 1)
				{
					if (levelsTrace[depth - 1] && index != count - 1)
						std::cout << (char)204;
					else
						std::cout << (char)200;
					std::cout << (char)205 << (char)205 << (char)205;
				}
				else
				{
					if (levelsTrace[i])
						std::cout << (char)186;
					else
						std::cout << " ";
					std::cout << "   ";
				}
			}
			std::cout << data[index] << std::endl;
			if (depth < levelsTrace.size())
				levelsTrace[depth] = 1;
			else
				levelsTrace += 1;
			printHeap(2 * index + 1, depth + 1, levelsTrace);
			levelsTrace[depth] = 0;
			printHeap(2 * index + 2, depth + 1, levelsTrace);
		}
	}

	Heap::Heap(const std::string & fileName)
	{
		std::ifstream fin(fileName);
		if (fin.good())
		{
			fin >> count;
			if (count > 0)
			{
				data = new long[count];
				for (long i = 0; i < count; ++i)
					fin >> data[i];
				for (long i = (count - 2)/2; i >= 0; --i)
					sortDown(i);
			}
		}
		fin.close();
	}

	bool Heap::contains(long value) const
	{
		if (count == 0)
			return false;
		return searchNode(value, 0);
	}

	void Heap::show() const
	{
		if (count > 0)
		{
			std::string levelsTrace = "";
			printHeap(0, 0, levelsTrace);
		}
		else
			std::cout << "Kopiec pusty.\n";
	}
	
	void Heap::clear()
	{
		if (data != nullptr)
		{
			delete[] data;
			data = nullptr;
			count = 0;
		}
	}

	void Heap::add(long value)
	{
		if (data == nullptr)
		{
			data = new long[1];
			count = 1;
			data[0] = value;
		}
		else
		{
			long * newData = new long[count + 1];
			for (long i = count - 1; i >= 0; --i)
				newData[i] = data[i];
			newData[count] = value;
			delete[] data;
			data = newData;
			sortUp(count++);
		}
	}

	void Heap::erase(long key)
	{
		if (key >= 0 && key < count)
		{
			data[key] = data[--count];
			long * newData = new long[count];
			for (long i = 0; i < count; ++i)
				newData[i] = data[i];
			delete[] data;
			data = newData;
			sortDown(key);
		}
	}

	long Heap::operator[](long index) const
	{
		if (index >= 0 && index < count)
			return data[index];
		return 0;
	}

	long & Heap::operator[](long index)
	{
		if (index >= 0 && index < count)
			return data[index];
		return index;
	}

	std::ostream & operator<<(std::ostream & os, const Heap & heap)
	{
		for (long i = 0, n = heap.count - 1; i < n; ++i)
			os << heap.data[i] << ' ';
		os << heap.data[heap.count - 1] << std::endl;
		return os;
	}

	std::ofstream & operator<<(std::ofstream & of, const Heap & heap)
	{
		of << heap.count;
		for (long i = 0; i < heap.count; ++i)
			of << std::endl << heap.data[i];
		return of;
	}

	std::istream & operator>>(std::istream & is, Heap & heap)
	{
		is >> heap.count;
		if (heap.count > 0)
		{
			heap.data = new long[heap.count];
			for (long i = 0; i < heap.count; ++i)
				is >> heap.data[i];
			for (long i = (heap.count - 2) / 2; i >= 0; --i)
				heap.sortDown(i);
		}
		return is;
	}

	Heap::~Heap()
	{
		if (data)
			delete[] data;
	}
}