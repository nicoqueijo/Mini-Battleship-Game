using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniBattleshipGame
{
    public partial class Form1 : Form
    {
        private const int HITS_TO_SINK_ALL_SHIPS = 4;
        private const int OFFSET = 1;
        private const int ROWS = 4;
        private const int COLS = 4;

        private const int EMPTY = 0;
        private const int OCC_SHIP_1 = 1;
        private const int OCC_SHIP_2 = 2;

        private Random rand = new Random();
        private Label[,] boardLabels;
        private int[,] boardLabelsStatus;
        private int moves;
        private int hits;

        public Form1()
        {
            InitializeComponent();
        }

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

        private void placeFirstShip()
        {
            int randRow = rand.Next(ROWS);
            int randCol = rand.Next(COLS - OFFSET);
            boardLabelsStatus[randRow, randCol] = OCC_SHIP_1;
            boardLabelsStatus[randRow, randCol + OFFSET] = OCC_SHIP_1;
        }

        private void placeSecondShip()
        {
            int randRow = 0;
            int randCol = 0;
            do
            {
                randRow = rand.Next(ROWS - OFFSET);
                randCol = rand.Next(COLS);
            } while (boardLabelsStatus[randRow, randCol] != EMPTY || boardLabelsStatus[randRow + OFFSET, randCol] != EMPTY);
            boardLabelsStatus[randRow, randCol] = OCC_SHIP_2;
            boardLabelsStatus[randRow + OFFSET, randCol] = OCC_SHIP_2;
        }

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
                        case OCC_SHIP_1:
                            boardLabels[row, col].BackColor = Color.Maroon;
                            hits++;
                            break;
                        case OCC_SHIP_2:
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

        private void board_label_0_0_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(0, 0);
        }

        private void board_label_0_1_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(0, 1);
        }

        private void board_label_0_2_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(0, 2);
        }

        private void board_label_0_3_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(0, 3);
        }

        private void board_label_1_0_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(1, 0);
        }

        private void board_label_1_1_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(1, 1);
        }

        private void board_label_1_2_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(1, 2);
        }

        private void board_label_1_3_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(1, 3);
        }

        private void board_label_2_0_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(2, 0);
        }

        private void board_label_2_1_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(2, 1);
        }

        private void board_label_2_2_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(2, 2);
        }

        private void board_label_2_3_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(2, 3);
        }

        private void board_label_3_0_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(3, 0);
        }

        private void board_label_3_1_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(3, 1);
        }

        private void board_label_3_2_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(3, 2);
        }

        private void board_label_3_3_Click(object sender, EventArgs e)
        {
            handleBoardLabelClick(3, 3);
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            resetBoard();
        }
    }
}
