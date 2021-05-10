using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ImageEncryptCompress
{
    public class HuffmanTree
    {
        public static void getCode(Node current , String code , ref Dictionary<int,String> res)
        {
            if(current.Right==null && current.Left==null && current.Symbol!=-1)
            {
                if (res.ContainsKey(current.Symbol))
                {
                    res[current.Symbol] = code;
                }
                else
                {
                    res.Add(current.Symbol, code);
                }
                return;
            }
            if (current.Left != null)
            {
                getCode(current.Left, code + "0" , ref res);
            }
            if(current.Right!=null)
            {
                getCode(current.Right, code + "1" , ref res);
            }
        }
        public static Dictionary<int, String> getBinary(Dictionary<int, int> color, PriorityQueue<int, Node> Huffman)
        {
            //insert the frequencies as nodes
            foreach (KeyValuePair<int, int> entry in color)
            {
                int frequency = entry.Key;
                int counter = entry.Value;
                Node leaf = new Node(frequency);
                Huffman.Enqueue(counter, leaf);
            }

            //construct the tree
            while (Huffman.Count != 1)
            {
                System.Collections.Generic.KeyValuePair<int, Node> rightLeaf = Huffman.Dequeue();
                System.Collections.Generic.KeyValuePair<int, Node> leftLeaf = Huffman.Dequeue();

                int newKey = rightLeaf.Key + leftLeaf.Key;

                Node newValue = new Node(-1);
                newValue.Right = rightLeaf.Value;
                newValue.Left = leftLeaf.Value;

                Huffman.Enqueue(newKey, newValue);
            }

            System.Collections.Generic.KeyValuePair<int, Node> rootNode = Huffman.Dequeue();
            Node root = rootNode.Value;
            Dictionary<int, String> res = new Dictionary<int, String>();
            getCode(root, "" ,ref  res);

            return res;
        }

        public static void save_in_file(Dictionary<int, String> dictionary, Dictionary<int, int> dictionary2)
        {
            int key; //integer value to take the key in it
            int freq;
            String value; //string value to take the code in it
            FileStream fileStream = new FileStream("compress.txt", FileMode.Append); //make file stream to open or create a file then to write in it 
            StreamWriter streamWriter = new StreamWriter(fileStream); //make stream writer to enable me to write in the file

            foreach (KeyValuePair<int, string> item in dictionary) //loop to get every item in the dictionary that holds the codes and save it in the file
            {
                key = item.Key;
                value = item.Value;
                freq = dictionary2[key];

                streamWriter.Write(key);
                streamWriter.Write(" _ ");
                streamWriter.Write(freq);
                streamWriter.Write(" _ ");
                streamWriter.WriteLine(value);
            }
            streamWriter.Close();

        }

    }
}
