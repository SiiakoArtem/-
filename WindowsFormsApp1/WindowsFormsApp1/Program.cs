using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        public class PolarExplorerForm : Form
        {
            TextBox textBoxCrossingTimes;
            TextBox textBoxPolars;

            Button button;

            List<List<int>> text = new List<List<int>>();
            List<List<int>> firstSquares = new List<List<int>>();
            List<List<int>> secondSquares = new List<List<int>>();

            public PolarExplorerForm()
            {
                this.Text = "Полярники";
                this.Size = new Size(800, 600);

                textBoxPolars = new TextBox
                {
                    Location = new Point(10, 10),
                    Width = 200,
                    MaxLength = 1
                };
                this.Controls.Add(textBoxPolars);

                textBoxCrossingTimes = new TextBox
                {
                    Location = new Point(10, 40),
                    Width = 200 
                };
                this.Controls.Add(textBoxCrossingTimes);

                button = new Button
                {
                    Location = new Point(220, 10),
                    Text = "обрахувати"
                };
                button.Click += Button_Click;
                this.Controls.Add(button);

                
            }

            public void DrawPolar(int x, int y, int xSize , int ySize, int i)
            { 
                var panel = new Panel
                {
                    Size = new Size(xSize, ySize),
                    Location = new Point(x, y),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.LightBlue
                };

                var label = new Label
                {
                    Text = i.ToString(),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                panel.Controls.Add(label);
                this.Controls.Add(panel);
            }

            private void Button_Click(object sender, EventArgs e)
            {
                int n;
                int total_time = 0;

                try 
                {
                    int[] times = textBoxCrossingTimes.Text.Split().Select(int.Parse).ToArray();

                    if (times.Length != double.Parse(textBoxPolars.Text))
                    {
                        MessageBox.Show("Кількість полярників не відопвідають кількості часу переправи полярників!");

                    }
                    else
                    {
                        if (times.Length >= 3 && times.Length <= 100)
                        {
                            for (int i = 0; i < times.Length; i++)
                            {
                                DrawPolar(300 + i * 60, 10, 50, 50, times[i]);
                            }

                            Array.Sort(times);

                            while (times.Length > 3)
                            {
                                int option1 = times[1] + times[0] + times[times.Length - 1] + times[1];
                                int option2 = times[times.Length - 1] + times[0] + times[times.Length - 2] + times[0];
                                if (option1 <= option2)
                                {
                                    firstSquares.Add(new List<int> { times[0], times[1] });
                                    firstSquares.Add(new List<int> { times[times.Length - 2], times[times.Length - 1] });
                                    Console.WriteLine(times[1]);
                                    text.Add(new List<int> { times[1] });
                                    text.Add(new List<int> { times[0] });
                                    text.Add(new List<int> { times[times.Length - 1] });
                                    text.Add(new List<int> { times[1] });
                                    secondSquares.Add(new List<int> { times[1] });
                                    secondSquares.Add(new List<int> { times[times.Length - 1], times[times.Length - 2] });

                                }
                                else
                                {
                                    firstSquares.Add(new List<int> { times[0], times[times.Length - 1] });
                                    firstSquares.Add(new List<int> { times[0], times[times.Length - 2] });
                                    text.Add(new List<int> { times[times.Length - 1] });
                                    text.Add(new List<int> { times[0] });
                                    text.Add(new List<int> { times[times.Length - 2] });
                                    text.Add(new List<int> { times[0] });
                                    secondSquares.Add(new List<int> { times[times.Length - 1] });
                                    secondSquares.Add(new List<int> { times[times.Length - 2] });

                                }

                                total_time += Math.Min(option1, option2);
                                times = times.Take(times.Length - 2).ToArray();
                            }
                            if (times.Length == 3)
                            {
                                firstSquares.Add(new List<int> { times[0], times[2] });
                                text.Add(new List<int> { times[2] });
                                secondSquares.Add(new List<int> { times[2] });


                                text.Add(new List<int> { times[0] });
                                firstSquares.Add(new List<int> { times[0], times[1] });
                                text.Add(new List<int> { times[1] });
                                secondSquares.Add(new List<int> { times[0], times[1] });


                                total_time += times[0] + times[1] + times[2];
                            }
                            else if (times.Length == 2)
                            {
                                firstSquares.Add(new List<int> { times[0], times[1] });
                                text.Add(new List<int> { times[1] });
                                secondSquares.Add(new List<int> { times[0], times[1] });

                                total_time += times[1];
                            }

                            int y = 115;

                            foreach (var list in text)
                            {
                                for (int i = 0; i < list.Count; i++)
                                {
                                    DrawPolar(150 + i * 60, y, 125, 10, list[i]);
                                }

                                y += 50;
                            }

                            y = 100;

                            foreach (var list in firstSquares)
                            {
                                for (int i = 0; i < list.Count; i++)
                                {
                                    DrawPolar(10 + i * 60, y, 50, 50, list[i]);
                                }

                                y += 100;
                            }

                            y = 100;

                            foreach (var list in secondSquares)
                            {
                                for (int i = 0; i < list.Count; i++)
                                {
                                    DrawPolar(300 + i * 60, y, 50, 50, list[i]);
                                }

                                y += 100;
                            }
                            MessageBox.Show("Мінімальний час переправи: " + total_time);
                        }
                        else
                        {
                            MessageBox.Show("Введіть коректну кількість полярників і час переправи");
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Введіть число!");
                }
            }
        }

        [STAThread]
        static void Main()
        {
            PolarExplorerForm form = new PolarExplorerForm();
            form.AutoScroll = true;
            Application.Run(form);
        }
    }
}
