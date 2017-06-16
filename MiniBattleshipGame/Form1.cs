using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Author: Nicolas Queijo

/// <summary>
/// Program where the user can play the classic Battleship game. In this version of the game,
/// the computer will display a 4x4 grid of Labels in the screen, and the user must guess
/// where are the two hidden ships.
/// 
/// Each ship is of size 2 and one lays vertical while the other lays horizontal. They are
/// each displayed by a different shade of red. Water is obviously displayed by blue.
/// Undiscovered fields are displayed by gray.
/// 
/// The user can click on each of the labels, which will change their background color
/// depending on whether there’s a ship or just water under it. The game finishes when the
/// player has uncovered all the ships.
/// </summary>
namespace MiniBattleshipGame
{
    public partial class Form1 : Form
    {
        private const int HITS_TO_SINK_ALL_SHIPS = 4;
        private const int OFFSET = 1;
        private const int ROWS = 4;
        private const int COLS = 4;

        private const int EMPTY = 0;
        private const int OCCUPIED_BY_SHIP1 = 1;
        private const int OCCUPIED_BY_SHIP2 = 2;

        private Random rand = new Random();
        private Label[,] boardLabels;
        private int[,] boardLabelsStatus;
        private int moves;
        private int hits;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is the code that is executed when the window is loaded for the first time.
        /// The two-dimensional array of labels is initialised to the labels used to display
        /// the board. Also the two-dimensional array that tracks the status of each label
        /// is initialised to an empty structure. The resetBoard() method gets called to
        /// reset the board to its default configurations.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            boardLabels = new Label[ROWS, COLS]
            {
                {board_label_0_0, board_label_0_1, board_label_0_2, board_label_0_3},
                {board_label_1_0, board_label_1_1, board_label_1_2, board_label_1_3},
                {board_label_2_0, board_label_2_1, board_label_2_2, board_label_2_3},
                {board_label_3_0, board_label_3_1, board_label_3_2, board_label_3_3}
            };

            boardLabelsStatus = new int[ROWS, COLS];
            resetBoard();
        }

        /// <summary>
        /// This method sets the default game configurations. Each label is assigned the default
        /// gray color and the status of each is set to empty. Then it makes calls to the methods
        /// that randomly place two ships on the board. The variables to track the amount of
        /// moves and hits are reset to zero and the label that would display the results of the
        /// game is set to an empty string.
        /// </summary>
        private void resetBoard()
        {
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLS; col++)
                {
                    boardLabels[row, col].BackColor = Color.DarkGray;
                    boardLabelsStatus[row, col] = EMPTY;
                }
            }
            placeFirstShip();
            placeSecondShip();
            moves = 0;
            hits = 0;
            game_status_label.Text = "";
        }

        /// <summary>
        /// Places the first ship in a random and empty location in the board assuring the ship
        /// fits within the bounds of the board.
        /// </summary>
        private void placeFirstShip()
        {
            int randRow = rand.Next(ROWS);
            int randCol = rand.Next(COLS - OFFSET);
            boardLabelsStatus[randRow, randCol] = OCCUPIED_BY_SHIP1;
            boardLabelsStatus[randRow, randCol + OFFSET] = OCCUPIED_BY_SHIP1;
        }

        /// <summary>
        /// Places the second ship in a random and empty location in the board assuring the ship
        /// fits within the bounds of the board. The difference is that it checks that the selected
        /// random location does not overlap with the first ship that should already be in place.
        /// </summary>
        private void placeSecondShip()
        {
            int randRow = 0;
            int randCol = 0;
            do
            {
                randRow = rand.Next(ROWS - OFFSET);
                randCol = rand.Next(COLS);
            } while (boardLabelsStatus[randRow, randCol] != EMPTY || boardLabelsStatus[randRow + OFFSET, randCol] != EMPTY);
            boardLabelsStatus[randRow, randCol] = OCCUPIED_BY_SHIP2;
            boardLabelsStatus[randRow + OFFSET, randCol] = OCCUPIED_BY_SHIP2;
        }

        /// <summary>
        /// Checks if the game has not finished yet. If that is the case then it checks that the
        /// label that was clicked is an undiscovered label (gray colour). If so, then it checks
        /// if there is water, or a ship under it. Then it changes the colour of the label
        /// accordingly and increases the hits variable if a ship was under that label. Finally,
        /// it checks if all the ships were sank and if so a message is displayed informing the user
        /// the game was won in the current number of hits.
        /// </summary>
        /// <param name="row">The row of the label that was clicked</param>
        /// <param name="col">The column of the label that was clicked</param>
        private void handleBoardLabelClick(int row, int col)
        {
            if (hits < HITS_TO_SINK_ALL_SHIPS)
            {
                if (boardLabels[row, col].BackColor == Color.DarkGray)
                {
                    int labelStatus = boardLabelsStatus[row, col];
                    switch (labelStatus)
                    {
                        case EMPTY:
                            boardLabels[row, col].BackColor = Color.DodgerBlue;
                            break;
                        case OCCUPIED_BY_SHIP1:
                            boardLabels[row, col].BackColor = Color.Maroon;
                            hits++;
                            break;
                        case OCCUPIED_BY_SHIP2:
                            boardLabels[row, col].BackColor = Color.Firebrick;
                            hits++;
                            break;
                    }
                    moves++;
                    if (hits >= HITS_TO_SINK_ALL_SHIPS)
                    {
                        game_status_label.Text = "You won in " + moves + " moves!";
                    }
                }
            }  
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_0_0_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(0, 0);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_0_1_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(0, 1);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_0_2_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(0, 2);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_0_3_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(0, 3);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_1_0_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(1, 0);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_1_1_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(1, 1);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_1_2_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(1, 2);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_1_3_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(1, 3);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_2_0_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(2, 0);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_2_1_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(2, 1);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_2_2_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(2, 2);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_2_3_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(2, 3);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_3_0_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(3, 0);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_3_1_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(3, 1);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_3_2_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(3, 2);
        }

        /// <summary>
        /// Calls the method that is responsible for handling the click events passing it the
        /// appropriate coordinates of this label.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void board_label_3_3_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(3, 3);
        }

        /// <summary>
        /// Resets the board when the Start button is called.
        /// </summary>
        /// <param name="sender">Unused but required</param>
        /// <param name="e">Unused but required</param>
        private void start_button_Click(object sender, EventArgs e)
        {
            resetBoard();
        }
    }
}
