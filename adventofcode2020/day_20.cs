using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace adventofcode2020
{
    public static class DayTwenty
    {
        public static void Execute()
        {
            ProblemOne();
            ProblemTwo();
        }

        public static void ProblemOne()
        {
            // load in all tiles into a List<Tile> (id, list of string representing picture)
            List<Tile> tiles = new List<Tile>();
            Queue<string> input = new Queue<string>(File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "input", "day_20.txt")).ToList());

            while (input.Count > 0)
            {
                // dequeue and get ID, create a new object
                var tileLine = input.Dequeue().Split("Tile ");
                int tileId = int.Parse(tileLine[1].TrimEnd(':'));

                // while next line not empty, build up the picture
                List<string> picture = new List<string>();
                while (input.Count > 0 && !string.IsNullOrEmpty(input.Peek()))
                {
                    picture.Add(input.Dequeue());
                }

                // create the tile and add it to the list of tiles.
                tiles.Add(new Tile(tileId, picture));

                // remove the spacer
                if(input.Count > 0) input.Dequeue();
            }

            // determine size of output picture and store this data in a List<Tile> (square root of the number is the size of the big pic)
            int bigPictureSize = Convert.ToInt32(Math.Sqrt(tiles.Count));

            List<Tile> foundTiles = new List<Tile>();

            // start looking for matches... alter the second if it doesn't match
            List<Tile> outerLoop = tiles;            
            for (int i = 0; i < outerLoop.Count; i++)
            {
                Tile first = outerLoop.ElementAt(i);
                for (int j = 0; j < tiles.Count; j++)
                {
                    Tile second = tiles.ElementAt(j);
                    if (first != second)
                    {
                        TileOrientation currentTileOrientation = 0;
                        bool match = false;
                        while (!match && Enum.IsDefined(typeof(TileOrientation), currentTileOrientation))
                        {
                            match = TryMatch(first, second);

                            try
                            {
                                currentTileOrientation++;

                                switch (currentTileOrientation)
                                {
                                    case TileOrientation.Rotate90:
                                        second.RotatePicture();
                                        break;
                                    case TileOrientation.Rotate180:
                                        second.RotatePicture();
                                        break;
                                    case TileOrientation.Rotate270:
                                        second.RotatePicture();
                                        break;
                                    case TileOrientation.Flip:
                                        second.RotatePicture();
                                        second.FlipPicture();
                                        break;
                                    case TileOrientation.FlipAndRotate90:
                                        second.RotatePicture();
                                        break;
                                    case TileOrientation.FlipAndRotate180:
                                        second.RotatePicture();
                                        break;
                                    case TileOrientation.FlipAndRotate270:
                                        second.RotatePicture();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            catch { }
                        }

                        if (match)
                        {
                            if (!foundTiles.Any(x => x.ID == first.ID))
                            {
                                foundTiles.Add(first);
                            }
                            if (!foundTiles.Any(x => x.ID == second.ID))
                            {
                                foundTiles.Add(second);
                            }

                            if (foundTiles.Count > 1)
                            {
                                outerLoop = foundTiles;
                            }
                            else
                            {
                                outerLoop = tiles;
                            }
                        }
                    }
                }
            }
            
            // all tiles should be in the correct rotations now, let's go back through them and find other matches
            for (int i = 0; i < tiles.Count; i++)
            {
                Tile first = tiles.ElementAt(i);
                for (int j = 0; j < tiles.Count; j++)
                {
                    Tile second = tiles.ElementAt(j);
                    if (first != second)
                    {
                        TryMatch(first, second);
                    }
                }
            }

            // after all have been "found", iterate through the found tiles to figure out the rest of the tiles it matches. 
            int[,] bigPicture = ValidateBigPicture(bigPictureSize, tiles);

            // calculate the product (use a long) of the four tiles in the corners (those ones will be have null top/left, top/right, bottom/left, bottom/right values)
            long product = (long)bigPicture[0, 0] * (long)bigPicture[0, bigPictureSize - 1] * (long)bigPicture[bigPictureSize - 1, 0] * (long)bigPicture[bigPictureSize - 1, bigPictureSize - 1];
            Console.WriteLine($"Product of corners: {product}");
        }

        public static void ProblemTwo()
        {

        }        

        private static int[,] ValidateBigPicture(int sideLength, List<Tile> tiles)
        {
            int[,] bigPicture = new int[sideLength, sideLength];

            for(int i = 0; i < sideLength; i++)
            {
                for(int j = 0; j < sideLength; j++)
                {
                    // top row
                    if (i == 0)
                    {
                        // top left corner
                        if (j == 0)
                        {
                            var topLeftCornerTiles = tiles.Where(x => x.TopMatchID == null && x.LeftMatchID == null).ToList();
                            if (topLeftCornerTiles.Count == 1)
                            {
                                bigPicture[i, j] = topLeftCornerTiles.First().ID;
                            }
                            else
                            {
                                throw new Exception($"Found {topLeftCornerTiles.Count} tiles for the top/left corner.");
                            }
                        }
                        // inner tiles
                        else if (j > 0 && j < sideLength - 1)
                        {
                            var nextTiles = tiles.Where(x => x.TopMatchID == null && x.LeftMatchID == bigPicture[i, j - 1]).ToList();
                            if (nextTiles.Count == 1)
                            {
                                bigPicture[i, j] = nextTiles.First().ID;
                            }
                            else
                            {
                                throw new Exception($"Found {nextTiles.Count} tiles for the next tile.");
                            }
                        }
                        // top right corner
                        else if (j == sideLength - 1)
                        {
                            var topRightCornerTiles = tiles.Where(x => x.TopMatchID == null && x.RightMatchID == null).ToList();
                            if (topRightCornerTiles.Count == 1)
                            {
                                bigPicture[i, j] = topRightCornerTiles.First().ID;
                            }
                            else
                            {
                                throw new Exception($"Found {topRightCornerTiles.Count} tiles for the top/right corner.");
                            }
                        }
                    }
                    // inner rows
                    else if (i > 0 && i < sideLength - 1)
                    {
                        // left side
                        if (j == 0)
                        {
                            var leftSideTiles = tiles.Where(x => x.TopMatchID == bigPicture[i - 1, j] && x.LeftMatchID == null).ToList();
                            if (leftSideTiles.Count == 1)
                            {
                                bigPicture[i, j] = leftSideTiles.First().ID;
                            }
                            else
                            {
                                throw new Exception($"Found {leftSideTiles.Count} tiles for the left side.");
                            }
                        }
                        // match sides and top
                        else if (j > 0 && j < sideLength - 1)
                        {
                            var nextTiles = tiles.Where(x => x.TopMatchID == bigPicture[i - 1, j] && x.LeftMatchID == bigPicture[i, j - 1]).ToList();
                            if (nextTiles.Count == 1)
                            {
                                bigPicture[i, j] = nextTiles.First().ID;
                            }
                            else
                            {
                                throw new Exception($"Found {nextTiles.Count} tiles for the next tile.");
                            }
                        }
                        // right side
                        else if (j == sideLength - 1)
                        {
                            var rightSideTiles = tiles.Where(x => x.TopMatchID == bigPicture[i - 1, j] && x.RightMatchID == null).ToList();
                            if (rightSideTiles.Count == 1)
                            {
                                bigPicture[i, j] = rightSideTiles.First().ID;
                            }
                            else
                            {
                                throw new Exception($"Found {rightSideTiles.Count} tiles for the right side.");
                            }
                        }
                    }
                    // bottom row
                    else if(i == sideLength - 1)
                    {
                        // bottom left corner
                        if (j == 0)
                        {
                            var bottomLeftCornerTiles = tiles.Where(x => x.TopMatchID == bigPicture[i - 1, j] && x.LeftMatchID == null && x.BottomMatchID == null).ToList();
                            if (bottomLeftCornerTiles.Count == 1)
                            {
                                bigPicture[i, j] = bottomLeftCornerTiles.First().ID;
                            }
                            else
                            {
                                throw new Exception($"Found {bottomLeftCornerTiles.Count} tiles for the bottom/left corner.");
                            }
                        }
                        // match sides and top
                        else if (j > 0 && j < sideLength - 1)
                        {
                            var nextTiles = tiles.Where(x => x.TopMatchID == bigPicture[i - 1, j] && x.LeftMatchID == bigPicture[i, j - 1] && x.BottomMatchID == null).ToList();
                            if (nextTiles.Count == 1)
                            {
                                bigPicture[i, j] = nextTiles.First().ID;
                            }
                            else
                            {
                                throw new Exception($"Found {nextTiles.Count} tiles for the next tile.");
                            }
                        }
                        // bottom right corner
                        else if (j == sideLength - 1)
                        {
                            var bottomRightCornerTiles = tiles.Where(x => x.TopMatchID == bigPicture[i - 1, j] && x.RightMatchID == null && x.BottomMatchID == null).ToList();
                            if (bottomRightCornerTiles.Count == 1)
                            {
                                bigPicture[i, j] = bottomRightCornerTiles.First().ID;
                            }
                            else
                            {
                                throw new Exception($"Found {bottomRightCornerTiles.Count} tiles for the bottom/right corner.");
                            }
                        }
                    }
                }
            }

            return bigPicture;
        }

        private static bool TryMatch(Tile first, Tile second)
        {
            if (first.GetBottomEdge() == second.GetTopEdge())
            {
                first.BottomMatchID = second.ID;
                second.TopMatchID = first.ID;
                return true;
            }
            else if (first.GetRightEdge() == second.GetLeftEdge())
            {
                first.RightMatchID = second.ID;
                second.LeftMatchID = first.ID;
                return true;
            }
            else if (first.GetTopEdge() == second.GetBottomEdge())
            {
                first.TopMatchID = second.ID;
                second.BottomMatchID = first.ID;
                return true;
            }
            else if (first.GetLeftEdge() == second.GetRightEdge())
            {
                first.LeftMatchID = second.ID;
                second.RightMatchID = first.ID;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public enum TileOrientation
    {
        Original = 0, 
        Rotate90 = 1, 
        Rotate180 = 2, 
        Rotate270 = 3, 
        Flip = 4, 
        FlipAndRotate90 = 5,
        FlipAndRotate180 = 6, 
        FlipAndRotate270 = 7
    }

    public class Tile
    {
        public Tile(int id, List<string> picture)
        {
            ID = id;
            Picture = picture;               
        }

        [Required]
        public int ID { get; private set; }

        [Required]
        public List<string> Picture { get; private set; }

        public int? TopMatchID { get; set; }
        public int? BottomMatchID { get; set; }
        public int? LeftMatchID { get; set; }
        public int? RightMatchID { get; set; }

        private string _rightEdge;
        private string _leftEdge;
        private string _topEdge;
        private string _bottomEdge;

        public string GetRightEdge()
        {
            if(string.IsNullOrEmpty(_rightEdge))
            {
                _rightEdge = "";

                foreach (var line in Picture)
                {
                    _rightEdge += line[^1];
                }
            }

            return _rightEdge;
        }

        public string GetLeftEdge()
        {
            if (string.IsNullOrEmpty(_leftEdge))
            {
                _leftEdge = "";

                foreach (var line in Picture)
                {
                    _leftEdge += line[0];
                }

                //for (int i = Picture.Count - 1; i >= 0; i--)
                //{
                //    _leftEdge += Picture[i][0];
                //}
            }

            return _leftEdge;
        }

        public string GetTopEdge()
        {
            if (string.IsNullOrEmpty(_topEdge))
            {
                _topEdge = Picture[0];
            }

            return _topEdge;
        }

        public string GetBottomEdge()
        {
            if (string.IsNullOrEmpty(_bottomEdge))
            {
                _bottomEdge = Picture[^1];
            }

            return _bottomEdge;
        }

        /// <summary>
        /// Flips the picture vertically. 
        /// </summary>
        public void FlipPicture()
        {
            // don't allow if match found for any edge. 
            if (TopMatchID != null || BottomMatchID != null || RightMatchID != null || LeftMatchID != null)
            {
                throw new Exception("This Tile has already been matched to another Tile. Altering this picture cannot be done. ");
            }

            // on success, clear all private edge values. 
            _rightEdge = _leftEdge = _topEdge = _bottomEdge = null;

            List<string> flippedPicture = new List<string>();

            foreach(var l in Picture)
            {
                char[] larray = l.ToCharArray();
                Array.Reverse(larray);
                flippedPicture.Add(new string(larray));
            }

            Picture = new List<string>(flippedPicture);
        }

        /// <summary>
        /// Rotates 90 degrees to the right per turn.
        /// </summary>
        /// <param name="turns">How many times to rotate. Defaults to 1 turn</param>
        public void RotatePicture(int turns = 1)
        {
            // don't allow if match found for any edge. 
            if (TopMatchID != null || BottomMatchID != null || RightMatchID != null || LeftMatchID != null)
            {
                throw new Exception("This Tile has already been matched to another Tile. Altering this picture cannot be done. ");
            }

            // on success, clear all private edge values. 
            _rightEdge = _leftEdge = _topEdge = _bottomEdge = null;

            List<string> rotatedPicture = new List<string>();

            for(int i = 0; i < turns; i++)
            {
                for (int k = 0; k < Picture.Count; k++)
                {
                    string newLine = "";

                    for (int j = Picture.Count - 1; j >= 0; j--)
                    {
                        newLine += Picture[j][k];
                    }

                    rotatedPicture.Add(newLine);
                }
            }

            Picture = new List<string>(rotatedPicture);
        }
    }
}
