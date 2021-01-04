(Get-Content -Path ..\src\Collections\Numeric\ByteCollectionExtensions.cs -Raw) -creplace 'byte','char' -creplace 'Byte','Char' | Set-Content -Path ..\src\Collections\Numeric\CharCollectionExtensions.cs
(Get-Content -Path ..\src\Collections\Numeric\ByteCollectionExtensions.cs -Raw) -creplace 'byte','int' -creplace 'Byte','Int' | Set-Content -Path ..\src\Collections\Numeric\IntCollectionExtensions.cs
(Get-Content -Path ..\src\Collections\Numeric\ByteCollectionExtensions.cs -Raw) -creplace 'byte','long' -creplace 'Byte','Long' | Set-Content -Path ..\src\Collections\Numeric\LongCollectionExtensions.cs
(Get-Content -Path ..\src\Collections\Numeric\ByteCollectionExtensions.cs -Raw) -creplace 'byte','short' -creplace 'Byte','Short' | Set-Content -Path ..\src\Collections\Numeric\ShortCollectionExtensions.cs

Get-ChildItem -Path ..\tests\Collection.Tests\ByteCollectionExtensions -File -Filter *.cs | ForEach-Object {
    $filename = $_.Name
    $content = Get-Content -Path "..\tests\Collection.Tests\ByteCollectionExtensions\$filename"

    # $content -creplace 'byte','char' -creplace 'Byte','Char' | Set-Content -Path ..\tests\Collection.Tests\CharCollectionExtensions\$_
    $content -creplace 'byte','int' -creplace 'Byte','Int' | Set-Content -Path ..\tests\Collection.Tests\IntCollectionExtensions\$_
    $content -creplace 'byte','long' -creplace 'Byte','Long' | Set-Content -Path ..\tests\Collection.Tests\LongCollectionExtensions\$_
    $content -creplace 'byte','short' -creplace 'Byte','Short' | Set-Content -Path ..\tests\Collection.Tests\ShortCollectionExtensions\$_
}
