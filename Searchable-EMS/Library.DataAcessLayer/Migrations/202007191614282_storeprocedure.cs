namespace Library.DataAcessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class storeprocedure : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "[dbo].[usp_GetEmployeList]",
                c => new
                {
                    Offset = c.Int(),
                    Limit = c.Int(),
                    DateFrom = c.DateTime(defaultValueSql: "NULL"),
                    DateTo = c.DateTime(defaultValueSql: "NULL"),
                    SearchText = c.String(maxLength: 300, defaultValueSql: "NULL"),
                    SalaryRangeFrom = c.Decimal(defaultValueSql: "NULL", precision: 18, scale: 2),
                    SalaryRangeTo = c.Decimal(defaultValueSql: "NULL", precision: 18, scale: 2),
                    Gender = c.Int(null, "null"),
                    ImportDateFrom = c.DateTime(defaultValueSql: "NULL"),
                    ImportDateTo = c.DateTime(defaultValueSql: "NULL")
                },
                body: @"SET NOCOUNT ON;
	SELECT COUNT(*) OVER()       AS RowTotal,
	       ROW_NUMBER() OVER(ORDER BY e.FullName ASC) AS RowNum,
	       e.Id,
	       e.FullName,
	       e.DateOfBirth,
	       g.Name                AS Gender,
	       e.Salary,
	       e.Designation
	FROM   dbo.Employee          AS e
	       LEFT JOIN dbo.Gender  AS g
	            ON  e.Gender = g.Id
	WHERE  (
	           (CAST(e.DateOfBirth AS DATE) BETWEEN @DateFrom AND @DateTo)
	           OR (@DateFrom IS NULL AND @DateTo IS NULL)
	           OR (@DateFrom = '' AND @DateTo = '')
	       )
	       AND (
	               (@SearchText IS NULL)
	               OR (e.FullName LIKE '%' + @SearchText + '%')
	           )
	       AND (
	               (e.Salary BETWEEN @SalaryRangeFrom AND @SalaryRangeTo)
	               OR (@SalaryRangeFrom IS NULL AND @SalaryRangeTo IS NULL)
	                  --OR (@SalaryRangeFrom = '' AND @SalaryRangeTo = '')
	           )
	       AND ((e.Gender = @Gender) OR (@Gender IS NULL OR (@Gender = 0)))
	       AND (
	               (CAST(e.ImportedDate AS DATE) BETWEEN @ImportDateFrom AND @ImportDateTo)
	               OR (@ImportDateFrom IS NULL AND @ImportDateTo IS NULL)
	               OR (@ImportDateFrom = '' AND @ImportDateTo = '')
	           )
	ORDER BY
	       e.FullName ASC
	       OFFSET(@Offset - 1) ROWS
	
	FETCH NEXT @Limit ROWS ONLY;
	
	SET NOCOUNT OFF;"
                );
        }

        public override void Down()
        {
            DropStoredProcedure("[dbo].[usp_GetEmployeList]");
        }
    }
}
