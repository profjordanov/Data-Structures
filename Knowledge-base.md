# Array Data Structures

- Ordered
- Very lightweight
- Has a fixed size
- Usually built into the language
- Many collections are implemented by using arrays
- Arrays use a single block of memory

  Example: (Int32 uses 4 bytes)

  ```csharp
  int[] array = { 2, 4, 1, 3, 5 };
  ```
  Total: 5 * 4 bytes

- Uses total of array pointer + (N * element/pointer size)
- Array Address + (Element Index * Size) = Element Address
- Array Element Lookup – O(1)
- Memory after the array may be occupied
- If we want to resize the array we have to make a copy
- Array Copy – O(n)
- Attributes: ComVisibleAttribute & SerializableAttribute
- Implements: ICollection, IEnumerable. IList. IStructuralComparable. IStructuralEquatable. ICloneable

# List Data Structure

- Has a variable size
- Implemented using an array
- Dynamic Arrays (Lists) – Add O(1)
- Doubles its capacity when needed
- Copying occurs at log(n)  n = 109, only ~30 copies

# Linked Lists
- Linked Lists are a chain of Nodes
- Add and Remove – O(1), if we have a pointer to the location

### Node
- Building block of many data structures
- A basic Node has a value and a pointer to the next node

  ```csharp
  Node<int> head = new Node(2);
  Node<int> next = new Node(5);

  head.Next(next);
  ```
  Simple example of class `Node`
  
  ```csharp
  public class Node<T>
  {
    public Node(T value)
    {
      this.Value = value;
    }
  
    public T Value { get; set; }
    public Node<T> Next { get; set; }
  }

  ```

# Stacks

- LIFO (Last In First Out) structure 
- Elements inserted (push) at the "top"
- Elements removed (pop) from the "top"

![alt text](http://bluegalaxy.info/codewalk/wp-content/uploads/2018/08/stack.jpg)

## Static Stack

- Has limited (fixed) capacity
- The current index (top) moves left / right with each pop / push
- Usually doubles its size (grows) when the capacity is filled

![alt text](http://www.introprogramming.info/wp-content/uploads/2013/07/clip_image0131.png)

## Generic Stack Class in .NET

- Implemented using an array
- Elements are of the same type T
- T can be any type, e.g. Stack<int> / Stack<Customer>
- Size is dynamically increased as needed (auto-grow)

### Basic Functionality
- Push(T) – inserts elements to the stack
- Pop() – removes and returns the top element from the stack
- Peek() – returns the top element without removing it
- Count – returns the number of elements in the stack
- Clear() – removes all elements
- Contains(T) – checks whether given element is in the stack
- ToArray() – converts the stack to an array
- TrimExcess() – trim the capacity to the actual space needed

  ```csharp
     // A simple stack of objects.  Internally it is implemented as an array,
    // so Push can be O(n).  Pop is O(1).
    [System.Runtime.InteropServices.ComVisible(false)]
    public class Stack<T> : IEnumerable<T>, 
        System.Collections.ICollection,
        IReadOnlyCollection<T> 
  ```
  
Simple Example:

  ```csharp
  static void Main()
  {
      Stack<string> stack = new Stack<string>();
      stack.Push("1. Ivan");
      stack.Push("2. Nikolay");
      stack.Push("3. Maria");
      Console.WriteLine("Top = {0}", stack.Peek());
      while (stack.Count > 0)
      {
          string personName = stack.Pop();
          Console.WriteLine(personName);
      }
  }
  ```
  
  ### Real-World Example
  - Undo operations 
    - Browser history
    - Chess game progress
  - Math expression evaluation
  - Implementation of function (method) calls
  - Tree-like structures traversal (DFS algorithm) 

# Queues
- FIFO (First In First Out) structure
- Elements inserted at the tail (enqueue)
- Elements removed from the head (dequeue)

![alt text](https://netmatze.files.wordpress.com/2014/08/queue.png)

## Linked Queue
- Dynamic (pointer-based) implementation
  - Each node has 2 fields: value and next
  - Dynamically create and delete objects
  
## Static (Circular) Queue
- Static (array-based) implementation
  - Implemented as a "circular array"
  - Has limited (fixed) capacity (doubled when filled)
  - Has head and tail indices, pointing to the head and the tail of the circular queue

## Generic Queue Class in .NET

- Queue<T> implements the queue data structure using a circular resizable array
  - Elements are of the same type T
  - T can be any type, e.g. / Queue<int> / Queue<DateTime>
  - Size is dynamically increased as needed
	
  ```csharp
     // A simple Queue of generic objects.  Internally it is implemented as a 
    // circular buffer, so Enqueue can be O(n).  Dequeue is O(1).
    [DebuggerTypeProxy(typeof(System_QueueDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]    
    [System.Runtime.InteropServices.ComVisible(false)]
    public class Queue<T> : IEnumerable<T>,
        System.Collections.ICollection,
        IReadOnlyCollection<T> 
  ```
	
### Basic Functionality
- Enqueue(T) – appends an element to the end of the queue
- Dequeue() – removes and returns the head element
- Peek() – returns the head element without removing it
- Other methods similar to the Stack<T> methods e.g. ToArray(), Contains(), etc
  
### Real-World Example  
- Operation system process scheduling
- Resource sharing
  - Printer document queue
  - Server requests queue
- Tree-like structures traversal (BFS algorithm) 

Example : Splits given array into batches

  ```csharp
		private static IEnumerable<Document[]> SplitDocumentsIntoBatches(Document[] documents, int batchSize)
		{
			var batchesCollection = new List<Document[]>();
			var batchQueue = new Queue<Document[]>(CommonExtensions.SplitArrayIntoBatches(documents, batchSize));

			// while the queue is not empty
			while(batchQueue.TryPeek(out var documentsToProcess))
			{
				// serialize the queue
				var documentsJson = JsonConvert.SerializeObject(documentsToProcess, Formatting.Indented);
				var documentsJsonBytes = Encoding.ASCII.GetBytes(documentsJson);
				// check if serialized text is larger than acceptable batch size
				if(documentsJsonBytes.Length > AcceptableBatchSize)
				{
					// split it into mulitple batches and enqueue them again
					var subDocumentBatches = CommonExtensions.SplitArrayIntoBatches(documentsToProcess, documentsToProcess.Length / 2)
						.ToList();

					foreach(var subPayslipBatch in subPayslipBatches)
					{
						batchQueue.Enqueue(subPayslipBatch);
					}
				}
				else
				{
					batchesCollection.Add(payslipsToProcess);
				}

				batchQueue.Dequeue();
			}

			return batchesCollection;
		} }
  }
  ```
  
# Tree-like Data Structures

- Branched recursive data structures
- Each node connected to other nodes
- Examples of tree-like structures
  - Trees: binary, balanced, ordered, etc.
  - Graphs: directed / undirected, weighted, etc.
  - Networks: graphs with particular attributes

## Recursive Tree Definition
- A single node is a tree
- Nodes have zero or multiple children that are also trees

  ```csharp
	public class Tree<T>
	{
	  private T value;
	  private IList<Tree<T>> children;
	  …
	}
  ```
