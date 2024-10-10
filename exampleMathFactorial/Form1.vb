Imports MySql.Data.MySqlClient

Public Class Form1
    Private Sub btnCalculateFactorial_Click(sender As Object, e As EventArgs) Handles btnCalculateFactorial.Click

        Dim num As Integer = Convert.ToInt32(txtNumber.Text)

        ' Connection string to connect to MySQL database
        Dim connectionString As String = "Server=localhost;Database=dbFactorial;User ID=root;Password=12Yellow34!"

        ' Create a new MySQL connection
        Using conn As New MySqlConnection(connectionString)
            Try
                ' Open the connection
                conn.Open()

                ' Create a MySQL command to call the stored procedure
                Using cmd As New MySqlCommand("calculate_factorial", conn)
                    cmd.CommandType = CommandType.StoredProcedure

                    ' Add input parameter for the number (num)
                    cmd.Parameters.AddWithValue("@num", num)

                    ' Add output parameter for the result (factorial)
                    Dim resultParam As New MySqlParameter("@result", MySqlDbType.Int64)
                    resultParam.Direction = ParameterDirection.Output
                    cmd.Parameters.Add(resultParam)

                    ' Execute the stored procedure
                    cmd.ExecuteNonQuery()

                    ' Retrieve and display the result
                    Dim result As Long = Convert.ToInt64(resultParam.Value)
                    MessageBox.Show("The factorial of " & num.ToString() & " is: " & result.ToString())
                End Using
            Catch ex As MySqlException
                MessageBox.Show("Error: " & ex.Message)
            Finally
                ' Close the connection
                conn.Close()
            End Try
        End Using

    End Sub
End Class
