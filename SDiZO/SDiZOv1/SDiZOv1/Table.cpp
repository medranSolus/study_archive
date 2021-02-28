#include "pch.h"
#include <fstream>
#include "Table.h"
namespace DataTypes
{
	Table::Table(const std::string & fileName)
	{
		std::ifstream fin(fileName);
		if (fin.good())
		{
			fin >> count;
			if (count)
			{
				data = new long[count];
				for (long i = 0; i < count; ++i)
					fin >> data[i];
			}
		}
		fin.close();
	}

	bool Table::contains(long value) const
	{
		for (long i = 0; i < count; ++i)
			if (data[i] == value)
				return true;
		return false;
	}

	void Table::clear()
	{
		if (data != nullptr)
		{
			delete[] data;
			data = nullptr;
			count = 0;
		}
	}

	void Table::popFront()
	{
		if (count == 1)
		{
			delete[] data;
			data = nullptr;
			count = 0;
		}
		else if (count > 1)
		{
			long * newData = new long[--count];
			for (long i = count; i > 0; --i)
				newData[i - 1] = data[i];
			delete[] data;
			data = newData;
		}
	}

	void Table::popBack()
	{
		if (count == 1)
		{
			delete[] data;
			data = nullptr;
			count = 0;
		}
		else if (count > 1)
		{
			long * newData = new long[--count];
			for (long i = 0; i < count; ++i)
				newData[i] = data[i];
			delete[] data;
			data = newData;
		}
	}

	void Table::erase(long index)
	{
		if (index == 0)
			popFront();
		else if (index == count - 1)
			popBack();
		else if (index > 0 && index < count)
		{
			long * newData = new long[count - 1];
			for (long i = 0; i < index; ++i)
				newData[i] = data[i];
			for (long i = index + 1; i < count; ++i)
				newData[i - 1] = data[i];
			delete[] data;
			data = newData;
			--count;
		}
	}

	void Table::pushFront(long value)
	{
		if (count == 0)
		{
			data = new long[1];
			data[0] = value;
			count = 1;
		}
		else
		{
			long * newData = new long[++count];
			newData[0] = value;
			for (long i = 1; i < count; ++i)
				newData[i] = data[i - 1];
			delete[] data;
			data = newData;
		}
	}

	void Table::pushBack(long value)
	{
		if (count == 0)
		{
			data = new long[1];
			data[0] = value;
			count = 1;
		}
		else
		{
			long * newData = new long[count + 1];
			for (long i = 0; i < count; ++i)
				newData[i] = data[i];
			newData[count++] = value;
			delete[] data;
			data = newData;
		}
	}

	void Table::insert(long index, long value)
	{
		if (index == 0)
			pushFront(value);
		else if (index == count - 1)
			pushBack(value);
		else if (index > 0 && index < count)
		{
			long * newData = new long[++count];
			for (long i = 0; i < index; ++i)
				newData[i] = data[i];
			newData[index] = value;
			for (long i = index + 1; i < count; ++i)
				newData[i] = data[i - 1];
			delete[] data;
			data = newData;
		}
	}

	long Table::operator[](long index) const
	{
		if (index >= 0 && index < count)
			return data[index];
		return 0;
	}

	long & Table::operator[](long index)
	{
		if (index >= 0 && index < count)
			return data[index];
		return index;
	}

	std::ostream & operator<<(std::ostream & os, const Table & table)
	{
		for (long i = 0, n = table.count - 1; i < n; ++i)
			os << table.data[i] << ' ';
		os << table.data[table.count - 1] << std::endl;
		return os;
	}

	std::ofstream & operator<<(std::ofstream & of, const Table & table)
	{
		of << table.count << std::endl;
		for (long i = 0, n = table.count - 1; i < n; ++i)
			of << table.data[i] << std::endl;
		of << table.data[table.count - 1];
		return of;
	}

	std::istream & operator>>(std::istream & is, Table & table)
	{
		table.clear();
		is >> table.count;
		if (table.count > 0)
		{
			table.data = new long[table.count];
			for (long i = 0; i < table.count; ++i)
				is >> table.data[i];
		}
		return is;
	}

	Table::~Table()
	{
		if (data != nullptr)
			delete[] data;
	}
}