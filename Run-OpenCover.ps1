function Run-CoverageAnalysis{
    param (
        [parameter(ValueFromPipeline=$true)]
        [System.IO.FileInfo[]] $project
    )

    Begin {
        Remove-Item .test -Recurse -Force -ea SilentlyContinue | Out-Null
        Remove-Item .coverage -Recurse -Force -ea SilentlyContinue | Out-Null
        New-Item .coverage -ItemType Directory | Out-Null
    }

    Process {
        Write-Output "--- $($project.FullName) ---"

        $name = $project.BaseName
        & $env:USERPROFILE\.nuget\packages\OpenCover\4.6.519\tools\OpenCover.Console.exe `
            -target:"C:\Program Files\dotnet\dotnet.exe" `
            -targetargs:"test -c release -o $PSSCRIPTROOT\.test\$name $project" `
            -register:user `
            -output:.coverage\coverage.xml `
            -searchDirs:.test\$name `
            -filter:"+[*]* -[*Tests]*" `
            -mergeoutput
    }

    End {
        & $env:USERPROFILE\.nuget\packages\ReportGenerator\2.5.11\tools\ReportGenerator.exe `
        -reports:.coverage\coverage.xml `
        -targetdir:.coverage `
        -verbosity:Error

        Write-Output "Report should be available at .coverage\index.html"
    }
}

gci *Tests.csproj -Recurse | Run-CoverageAnalysis