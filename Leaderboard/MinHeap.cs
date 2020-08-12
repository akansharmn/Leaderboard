using System;
using System.Collections.Generic;
using System.Linq;

namespace Leaderboard
{  
    public class MinHeap<T> where T: IComparable<T>
    {
        
        int count;
        List<T> list;
        Dictionary<T, int> set; 
        public MinHeap(int count, IEqualityComparer<T> comparer)
        {
            this.count = count;
            this.list = new List<T>();
            this.set = new Dictionary<T, int>(comparer);
        }

        public int Count => list.Count;
        public List<T> GetElements()
        {
            return list.Select(x => x).ToList();
        }

        public bool Add(T item)
        {
            if (set.ContainsKey(item))
            {
               return UpdateItem(item, set[item]);
            }
            else if (list.Count < count)
            {
                list.Add(item);
                set.Add(item, list.Count - 1);
                HeapifyUp(list.Count - 1);
                return true;
            } else
            {
                if (list.Count > 0)
                {
                    var top = GetTop();
                    if (top.CompareTo(item) < 0)
                    {
                        RemoveTop();
                        list.Add(item);
                        set.Add(item, list.Count - 1);
                        HeapifyUp(list.Count - 1);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool UpdateItem(T item, int index)
        {
            if(list[index].CompareTo(item)<0)
            {
                list[index] = item;
                HeapifyUp(index);
                return true;
            }
            else if (list[index].CompareTo(item) > 0)
            {
                list[index] = item;
                HeapifyDown(index);
                return true;
            }
            else
            {
                return false;
            }

        }

        public T RemoveTop()
        {
            if (list.Count == 0)
                throw new Exception("heap empty");

            var item = list[0];
            Swap( 0, list.Count - 1);
           
            set[list[0]] = 0;
            list.RemoveAt(list.Count - 1);
            set.Remove(list[list.Count - 1]);
            HeapifyDown(0);
            return item;
        }

        private void HeapifyDown(int i)
        {
            var left = i * 2 + 1;
            var right = i * 2 + 2;
            var cur = i;

            if (left < list.Count && list[left].CompareTo(list[cur]) > 0)
                cur = left;
            if (right < list.Count && list[right].CompareTo(list[cur]) > 0)
                cur = right;

            if (cur != i)
            {
                Swap(cur, i);
                set[list[cur]] = i;
                set[list[i]] = cur;
                HeapifyDown(cur);
            }

        }

        private void Swap( int v1, int v2)
        {
            var temp = list[v1];
            list[v1] = list[v2];
            list[v2] = temp;
        }

        private void HeapifyUp(int v)
        {
            var parent = Parent(v);

            while (parent != -1)
            {
                if (list[parent].CompareTo(list[v])<0)
                {
                    Swap(v, parent);
                    set[list[v]] = parent;
                    set[list[parent]] = v; 
                   
                }

                v = parent;
                parent = Parent(v);
            }
        }

        private int Parent(int v)
        {
            if (v == 0)
                return -1;
            return (v - 1) / 2;

        }

        public T GetTop()
        {
            if (list.Count == 0)
                throw new Exception("Heap Empty");
            return list[0];
        }
    }
}
