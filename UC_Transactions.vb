Imports MySql.Data.MySqlClient


Public Class UC_Transactions
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=password1234567890//;database=banking_system;")


    Private Sub UC_Transactions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTransactionsData()
    End Sub


    Private Sub LoadTransactionsData()
        Try
            conn.Open()
            Dim query As String = "SELECT * FROM transactions"
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim table As New DataTable()
            adapter.Fill(table)

            ' Bind data to DataGridView
            Guna2DataGridView1.DataSource = table
        Catch ex As Exception
            MessageBox.Show("Error loading accounts data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Guna2Button9_Click(sender As Object, e As EventArgs) Handles Guna2Button9.Click
        Dim parentForm = CType(Me.ParentForm, Form3)
        parentForm.LoadUserControl(New UC_CreateTransaction())

    End Sub
End Class
