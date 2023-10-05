using ETL_ProductionLine_Report.Models;
using ETL_ProductionLine_Report.Services;

namespace ETL_ProductionLine_Report.Tests;

public class ReportServiceTests
{
    [Fact]
    public void GetListOfDatesFromDataset_ReturnsListOfDates()
    {
        // Arrange
        List<string[]> TestSet = new List<string[]>{
            new string[] {"aa", "2022-02-21" , "cc", "dd"},
            new string[] {"ee", "2022-02-22", "gg", "hh"},
            new string[] {"ii", "2022-03-04", "kk", "ll"}
            };
/*
        List<string[]> TestSet = new List<string[]>{
            new string[] {"aa", "2022-02-21 14:52:48" , "cc", "dd"},
            new string[] {"ee", "2022-02-22 00:00:00", "gg", "hh"},
            new string[] {"ii", "2022-03-04 14:52:48", "kk", "ll"}
            };
*/
        List<string> expected = new List<string> {
            "2022-02-21",
            "2022-02-22",
            "2022-03-04"
        };

        // Act
        ReportService repService = new();
        List<string> actual = repService.GetListOfDatesFromDataset(TestSet, 1);
        
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetReportForGivenDay_ReturnsDailyReportForGivenDay() {

        // Arrange
        List<string[]> TestSet = new List<string[]>{
            new string[] {"2022-02-21", "40", "10" },
            new string[] {"2022-02-21", "40", "5" },
            new string[] {"2022-03-04", "40", "100" },
            new string[] {"2022-02-21", "20", "3" }
            };
            
        // Act
        ReportService service = new();
        ReportDaily actual = service.GetReportForGivenDay(TestSet, "2022-02-21", 0);

        // Assert
        Assert.Equal("2022-02-21", actual.Date);
        Assert.Equal("100", actual.PlannedOutput);
        Assert.Equal("18", actual.RealOutput);
        Assert.Equal("0.18", actual.PlanedPercentage);
    }

}