#ifndef  NODE_HPP
#define NODE_HPP

#include <vector>
#include <boost/shared_ptr.hpp>
#include "Varible.hpp"
#include "ICalculateMehtod.hpp"
#include "CalculateTreeEngine.hpp"
#include <string>
namespace free
{
	using namespace boost;
	using namespace std;
#define Shared_ptr boost::shared_ptr 

	class Node
	{
	private:
		Shared_ptr<Node> parent;
		Shared_ptr<Varible> self;
		vector<Node> param;
		ICalculateMethod method;
		int Index;

	public:
		Node(Shared_ptr<Varible> vari)
		{
			this->self = vari;
		}

		~Node()
		{
		}

		Node GetTopNode()
		{
			if (!parent)
			{

			}
		}

		Node GetNode(unsigned int index)
		{
			if (param.size()>index)
			{
				//param
			}
			throw new std::exception("ArgumentOutOfRangeException");
		}

		string GetParamDescription(int index)
		{
			if (param.size()>index)
			{
				return param[index].ToString();
			}
			throw new std::exception("ArgumentOutOfRangeException");
		}

		string ToString()
		{
			return "";
		}

		void SetParent()
		{
		
		}



	private:


		Node Clone(bool includeParam=true)
		{
			Node copy(this->self);
			if (includeParam)
			{

			}
			else
			{

			}
			return copy;
		}





	};



}

#endif // ! NODE_HPP
