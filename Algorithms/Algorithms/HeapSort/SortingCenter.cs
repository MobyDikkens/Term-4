/*  Sorting Center
*   The class that implements
*   HeapSort algorithm
*   
*
*   Version 0.1
*
*   Copyright Dima Sheludko 2018
 */


namespace HeapSort
{
    class SortingCenter
    {
        private int[] array = null;

        private int[] sorted = null;

        public int Permutations { get ; private set ; }

        public long Comparison { get ; private set ; } 

        public System.TimeSpan Execution { get ; private set ; }

        public long Assimptotic { get ; private set ;}

        public int[] Sort
        {
            get
            {
                if(this.sorted == null)
                {
                    this.Sortint();
                }

                return this.sorted;
            }
        }

        public int[] HeapSort
        {
            get
            {
                if(this.sorted == null)
                {
                    this.HeapSorting();
                }

                return this.sorted;
            }
        }

        public int[] Array
        {
            set
            {
                if((value != null) && (value.Length > 0))
                {
                    this.array = value;
                    this.sorted = null;
                }
            }
        }


        #region Constructors

        public SortingCenter()
        {
            this.array = null;
        }

        public SortingCenter(int[] array)
        {
            if((array != null) && (array.Length > 0))
            {
                this.array = array;
            }
        }


        #endregion



        #region Sorting Stuff

        //  HeapSort Algorithm
        private void Sortint()
        {
            if((this.array != null) && (this.array.Length > 0))
            {
                this.sorted = new int[array.Length];

                // Position of the deepest father
                int node = array.Length / 2 - 1;
                int lenth = this.array.Length;

                System.DateTime start = System.DateTime.Now;


                for(int i = array.Length; i > 0; i--)
                {
                    MaxToTop(lenth);

                    this.sorted[array.Length - lenth] = this.array[0];
                    //  swap top and bottom
                    this.array[0] = this.array[lenth - 1];
                    this.Permutations++;
                    lenth--;
                }

                this.Execution = System.DateTime.Now - start;

            }
        }

        private void MaxToTop(int lenth)
        {

            //  current father position
            int node = lenth / 2 - 1;

            int left = 0;
            int right = 0;

            int current = 0;

            while(node >= 0)
            {

                left = 2*node + 1;
                right = 2*node + 2;

                current = left;

                    //  if there are 2 childs
                    if(right < lenth)
                    {
                        //  if right > left
                        if(array[right] > array[left])
                        {
                            this.Comparison++;
                            current = right;
                        }  
                    }

                    if(array[current] > array[node])
                    {
                        //  swap
                        Swap(array, current, node);
                        this.Permutations+=2;
                    }

                    this.Comparison += 2;
                

                node--;

            }

        }

        private void Swap(int[] array, int i, int j)
        {
            array[i] += array[j];
            array[j] = array[i] - array[j];
            array[i] -= array[j];
        }


        #endregion




        #region Heap Sorting

        private void HeapSorting()
        {
            System.DateTime now = System.DateTime.Now;

            BuildTree(this.array);
            int lenth = this.array.Length;
            sorted = new int[this.array.Length];
            for(int i = 0; i < this.array.Length; i++)
            {
                sorted[i] = this.array[0];
                this.array[0] = this.array[lenth - 1];
                lenth--;
                Heapify(array,0,lenth);
            }

            this.Execution = System.DateTime.Now - now;

            this.Assimptotic += array.Length;

        }

        //   Build the binary sorting tree
        private void BuildTree(int[] array)
        {
            for(int i = array.Length/2 - 1; i >= 0; i--)
            {
                Heapify(this.array, i,this.array.Length);
            }
            var tmp = this.array;
        }

        // Abs the max element
        private void Heapify(int[] array, int node, int lenth)
        {
            int right = 2 * node + 2;
            int left = 2 * node + 1;
            int max = node;

            if((right < lenth) && (array[right] > array[max]))
            {
                max = right;
            }


            if((left < lenth) && (array[left] > array[max]))
            {
                max = left;
            }

            if(max != node)
            {
                this.Permutations++;
                this.Assimptotic++;
                Swap(array, max, node);
                //  max => node after swapping
                Heapify(array,max,lenth);
            }

            this.Comparison += 5;


        }



        #endregion


    }
    
}