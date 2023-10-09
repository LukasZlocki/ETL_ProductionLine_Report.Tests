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
    public void GetListOfDatesFromDataset_ReturnsListOfDatesWhenColumnNameApears()
    {
        // Arrange
        List<string[]> TestSet = new List<string[]>{
            new string[] {"aa", "This is column name" , "cc", "dd"},
            new string[] {"aa", "2022-02-21" , "cc", "dd"},
            new string[] {"ee", "2022-02-22", "gg", "hh"},
            new string[] {"ii", "2022-03-04", "kk", "ll"}
            };

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
            new string[] {"2022-02-21", "1000-1100", "40", "10" },
            new string[] {"2022-02-21", "1200-1300", "40", "5" },
            new string[] {"2022-03-04", "1400-1500", "40", "100" },
            new string[] {"2022-02-21", "1500-1600", "20", "3" }
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

    [Fact]
    public void GetListOfDailyReportsBaseOnDataset_ReturnsListOfDailyReportsForGivenDataset() {
        // Arrange
        List<string[]> TestSet = new List<string[]>{
            new string[] {"2022-02-21", "1000-1100", "40", "10" },
            new string[] {"2022-02-21", "1200-1300", "40", "5" },
            new string[] {"2022-02-21", "1500-1600", "20", "3" },
            new string[] {"2022-03-04", "1400-1500", "40", "40" },
            new string[] {"2022-03-04", "1600-1600", "20", "5" },
            new string[] {"2022-03-04", "1700-1800", "40", "20" }
            };

        // Act
        ReportService service = new();
        List<ReportDaily> actual = service.GetListOfDailyReportsBaseOnDataset(TestSet, 0);

        // Assert
        // first object
        Assert.Equal("2022-02-21", actual[0].Date);
        Assert.Equal("100",  actual[0].PlannedOutput);
        Assert.Equal("18",  actual[0].RealOutput);
        Assert.Equal("0.18",  actual[0].PlanedPercentage);
        // second object
        Assert.Equal("2022-03-04", actual[1].Date);
        Assert.Equal("100",  actual[1].PlannedOutput);
        Assert.Equal("65",  actual[1].RealOutput);
        Assert.Equal("0.65",  actual[1].PlanedPercentage);
    }

    [Fact]
    public void GetListOfDailyReportsBaseOnDataset_SkipRowWhenDatasetAreColumnDescriptions() {
        // Arrange
        List<string[]> TestSet = new List<string[]>{
            new string[] {"aaa", "bbb", "ccc", "ddd" },
            new string[] {"2022-02-21", "1200-1300", "40", "5" },
            new string[] {"2022-02-21", "1500-1600", "60", "3" },
            new string[] {"aaa", "bbb", "ccc", "ddd" },
            new string[] {"2022-03-04", "1600-1600", "60", "5" },
            new string[] {"2022-03-04", "1700-1800", "40", "20" }
            };

        // Act
        ReportService service = new();
        List<ReportDaily> actual = service.GetListOfDailyReportsBaseOnDataset(TestSet, 0);

        // Assert
        // first object
        Assert.Equal("2022-02-21", actual[0].Date);
        Assert.Equal("100",  actual[0].PlannedOutput);
        Assert.Equal("8",  actual[0].RealOutput);
        Assert.Equal("0.08",  actual[0].PlanedPercentage);
        // second object
        Assert.Equal("2022-03-04", actual[1].Date);
        Assert.Equal("100",  actual[1].PlannedOutput);
        Assert.Equal("25",  actual[1].RealOutput);
        Assert.Equal("0.25",  actual[1].PlanedPercentage);
    }
}