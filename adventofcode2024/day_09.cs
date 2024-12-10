using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using adventofcode.util;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace adventofcode2024
{
    public class day_9 : DayBase<long>
    {
        public day_9() : base("day_09.txt")
        {
            Console.WriteLine("Advent of Code - Day Nine");
        }

        public day_9(string fileName) : base(fileName) { }

        public override long Problem1()
        {
            string diskMap = _input[0];
            long checksum = 0;

            LinkedList<int> blocks = new LinkedList<int>();
            Queue<int> freeSpace = new Queue<int>();

            for (int i = 0; i < diskMap.Length; i += 2)
            {
                int blockCount = int.Parse(diskMap[i].ToString());
                blocks.AddLast(blockCount);

                if(i + 1 < diskMap.Length)
                {
                    int freeSpaceCount = int.Parse(diskMap[i + 1].ToString());
                    freeSpace.Enqueue(freeSpaceCount);
                }
            }

            int fileId = 0;
            int lastBlockFileId = blocks.Count - 1;
            int currentFreeSpace = 0;
            int lastBlockCount = 0;
            int checksumIndex = 0;

            while (blocks.Count > 0)
            {
                // get front block count
                int currentBlock = blocks.First.Value;
                blocks.RemoveFirst();

                // print them out
                for(int i = 0; i < currentBlock; i++)
                {
                    Console.Write(fileId);
                    checksum += fileId * checksumIndex++;
                }

                fileId++;

                // get the front free space count
                if (freeSpace.TryDequeue(out currentFreeSpace))
                {
                    while(currentFreeSpace > 0)
                    {
                        if(lastBlockCount == 0 && blocks.Count > 0)
                        {
                            lastBlockCount = blocks.Last.Value;
                            blocks.RemoveLast();
                        }

                        Console.Write(lastBlockFileId);
                        checksum += lastBlockFileId * checksumIndex++;

                        lastBlockCount--;
                        currentFreeSpace--;

                        if(lastBlockCount == 0)
                        {
                            lastBlockFileId--;
                        }
                    }
                }

            }

            if(lastBlockCount > 0)
            {
                for (int i = 0; i < lastBlockCount; i++)
                {
                    Console.Write(lastBlockFileId);
                    checksum += lastBlockFileId * checksumIndex++;
                }
            }

            return checksum;
        }


        public override long Problem2()
        {
            string diskMap = _input[0];
            long checksum = 0;

            List<(int id, int space)> blocks = new List<(int id, int space)>();
            List<int> freeSpace = new List<int>();

            for (int i = 0; i < diskMap.Length; i += 2)
            {
                int blockCount = int.Parse(diskMap[i].ToString());
                blocks.Add(((i / 2), blockCount));

                if (i + 1 < diskMap.Length)
                {
                    int freeSpaceCount = int.Parse(diskMap[i + 1].ToString());
                    freeSpace.Add(freeSpaceCount);
                }
            }

            int checksumIndex = 0;
            int endPointer = blocks.Count - 1;

            while (endPointer > 0)
            {
                if (freeSpace.Take(endPointer).All(x => x == 0))
                {
                    break;
                }

                // get last block count
                (int id, int space) lastBlock = blocks[endPointer];

                // find the first free space it will fit into
                int firstFreeSpaceIndex = freeSpace.FindIndex(0, x => x >= lastBlock.space);

                // add it into the list and decrease the free space size
                if (firstFreeSpaceIndex > -1 && firstFreeSpaceIndex < endPointer)
                {
                    // remove the block from the list
                    blocks.RemoveAt(endPointer);

                    // replace it with free space and combine with free space on either side
                    // we're at the end of the list
                    if (endPointer == freeSpace.Count)
                    {
                        int totalFreeSpaceToInsert = lastBlock.space;
                        totalFreeSpaceToInsert += freeSpace[endPointer - 1];

                        freeSpace.Insert(endPointer, totalFreeSpaceToInsert);

                        freeSpace.RemoveAt(endPointer - 1);
                    }
                    else
                    {
                        int totalFreeSpaceToInsert = lastBlock.space;
                        totalFreeSpaceToInsert += freeSpace[endPointer - 1];
                        totalFreeSpaceToInsert += freeSpace[endPointer];

                        freeSpace.Insert(endPointer, totalFreeSpaceToInsert);

                        freeSpace.RemoveAt(endPointer - 1);
                        freeSpace.RemoveAt(endPointer);
                    }

                    // insert the block into the list where the free space was found
                    blocks.Insert(firstFreeSpaceIndex + 1, lastBlock);

                    // insert 0 free space between the two blocks
                    freeSpace.Insert(firstFreeSpaceIndex, 0);

                    // decrease the free space found by the size of the block
                    freeSpace[firstFreeSpaceIndex + 1] -= lastBlock.space;

                }
                else
                {
                    endPointer--;
                }
            }

            for(int j = 0; j < blocks.Count; j++)
            {
                for(int k = 0; k < blocks[j].space; k++)
                {
                    Console.Write(blocks[j].id);
                    checksum += blocks[j].id * checksumIndex++;
                }

                for(int m = 0; m < freeSpace[j]; m++)
                {
                    Console.Write('.');
                    checksumIndex++;
                }
            }

            return checksum;
        }
    }
}
