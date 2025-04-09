Imports MySql.Data.MySqlClient
Public Class UC_Accounts
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=password1234567890//;database=banking_system;")


    Private Sub UC_Accounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCustomerData()
    End Sub

    Private Sub LoadCustomerData()
        Try
            conn.Open()
            Dim query As String = "SELECT * FROM accounts"
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim table As New DataTable()
            adapter.Fill(table)

            ' Bind data to DataGridView
            DataGridView1.DataSource = table
        Catch ex As Exception
            MessageBox.Show("Error loading accounts data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub

End Class
