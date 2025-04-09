Imports Guna.Charts.WinForms
Imports MySql.Data.MySqlClient

Public Class UC_Dashboard
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=password1234567890//;database=banking_system;")

    Private Sub UC_Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTransactionSummary()
        LoadAccountTypeChart()
    End Sub

    Private Sub LoadTransactionSummary()
        Try
            conn.Open()

            lblTotalDeposits.Text = GetTransactionCount("Deposit").ToString()
            lblTotalWithdrawals.Text = GetTransactionCount("Withdrawal").ToString()
            lblTotalInvestments.Text = GetTransactionCount("Investment").ToString()
            lblTotalLoans.Text = GetTransactionCount("Loan").ToString()

        Catch ex As Exception
            MessageBox.Show("Error retrieving summary: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Function GetTransactionCount(transactionType As String) As Integer
        Dim query As String = "SELECT COUNT(*) FROM transactions WHERE transaction_type = @type"
        Using cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@type", transactionType)
            Return Convert.ToInt32(cmd.ExecuteScalar())
        End Using
    End Function

    Private Sub LoadAccountTypeChart()
        Try
            conn.Open()

            ' List of account types
            Dim accountTypes() As String = {"Current Account", "Savings Account", "Loan Account", "Student Account", "Investment Account"}

            ' Clear previous chart data
            GunaChart1.Datasets.Clear()

            Dim dataset As New GunaBarDataset()
            dataset.Label = "Accounts Distribution"

            For Each accType As String In accountTypes
                Dim count As Integer = GetAccountTypeCount(accType)
                dataset.DataPoints.Add(accType, count)
            Next

            GunaChart1.Datasets.Add(dataset)
            GunaChart1.Update()

        Catch ex As Exception
            MessageBox.Show("Error loading chart: " & ex.Message, "Chart Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Function GetAccountTypeCount(accountType As String) As Integer
        Dim query As String = "SELECT COUNT(*) FROM accounts WHERE account_type = @type"
        Using cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@type", accountType)
            Return Convert.ToInt32(cmd.ExecuteScalar())
        End Using
    End Function
End Class
