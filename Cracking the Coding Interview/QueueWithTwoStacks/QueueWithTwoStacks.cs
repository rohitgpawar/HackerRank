using System;
using System.Collections.Generic;

/*
Implement Queue using two stacks. backStack will hold enqueued elements when Dequeue is called check frontStack which will hold front of the queue.
If frontStack is empty then pop all elements from backStack and push in frontStack, then Pop from frontStack.
So frontStack will hold elements in reverse order starting first element entered at the top of the stack and backStack will contain elements as they are enqueued.
     */

namespace HackerRank
{
    public class QueueWithTwoStacks
    {
        static Stack<int> frontStack = new Stack<int>();
        static Stack<int> backStack = new Stack<int>();

        /// <summary>
        /// Add element to backStack.
        /// </summary>
        /// <param name="val"></param>
        static void Enqueue(int val)
        {
            backStack.Push(val);
        }
        
        /// <summary>
        /// check if frontStack is empty. If empty then fill it with poping all elements from backStack. then Pop the first element in frontStack.
        /// </summary>
        /// <returns> top element from frontStack</returns>
        static int Dequeue()
        {
            if (frontStack.Count == 0)
            {
                while (backStack.Count > 0)
                {
                    frontStack.Push(backStack.Pop());
                }
            }

            return frontStack.Pop();
        }

        static int Peek()
        {
            if (frontStack.Count == 0)
            {
                while (backStack.Count > 0)
                {
                    frontStack.Push(backStack.Pop());
                }
            }
            return frontStack.Peek();
        }

        public static void QueueWithTwoStackMain()
        {
            int inst = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < inst; i++)
            {
                int[] instruction = Array.ConvertAll(Console.ReadLine().Split(' '), Int32.Parse);
                switch (instruction[0])
                {
                    case 1:
                        Enqueue(instruction[1]);
                        break;
                    case 2:
                        Dequeue();
                        break;
                    case 3:
                        Console.WriteLine(Peek());
                        break;
                }
            }
        }
    }
}
