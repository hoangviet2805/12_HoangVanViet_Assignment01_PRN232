Add-Type -AssemblyName System.IO.Compression.FileSystem
$zip = [System.IO.Compression.ZipFile]::OpenRead('d:\Tai_lieu\school\Tai_lieu\SE8\PRN232\Assignment1\PRN232Assignment01.docx')
$entry = $zip.Entries | Where-Object { $_.FullName -eq 'word/document.xml' }
$stream = $entry.Open()
$reader = New-Object System.IO.StreamReader($stream)
$xml = $reader.ReadToEnd()
$reader.Close()
$stream.Close()
$zip.Dispose()
$text = $xml -replace '<w:p[^>]*>', "`n" -replace '<[^>]+>', ''
$text | Out-File 'd:\Tai_lieu\school\Tai_lieu\SE8\PRN232\Assignment1\assignment_text.txt'
