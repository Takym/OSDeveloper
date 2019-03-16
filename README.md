# このブランチは最新ではありません。
# This branch is not latest.
# OSDeveloper IDE - the "locales" branch
Copyright (C) 2018 Takym.

## <a id="lang">言語/Language</a>
* [日本語](#ja_top)
* [English](#en_top)

* [CREDITS](#credits)

<!-- 日本語 -->
## <a id="ja_top">localesブランチについて</a>
- このブランチではOSDeveloperの翻訳版を提供します。
- *.resx、*.ja.resx、*.en.resxはオリジナルのOSDeveloperからコピーされてきた物です。
	- これらのファイルは参考の為に提供されています。変更しないでください。
		- 変更する場合はmasterブランチでお願いします。
	- *.resxは限定のリソースです。言語が指定されていない場合、又は、翻訳が存在しない場合に利用されています。
	- *.ja.resxは日本語版の翻訳リソースです。
	- *.en.resxは英語版の翻訳リソースです。
- 日本語版の翻訳リソースを作成/変更する場合は、*.ja-JP.resxで作成/変更してください。
- 米国英語版の翻訳リソースを作成/変更する場合は、*.en-US.resxで作成/変更してください。

## 注意事項
- マネージド系プロジェクト(C#)のresxファイルで言語リソースを作成します。
- ネイティブ系プロジェクト(C/C++)では言語リソースを追加する事はできません。
- 言語リソースを作成/変更した場合は、下記のCREDITSに名前とGitHubのアカウントページのURLを追加してください。
	- 他人の名前を削除しないでください。


<!-- English -->
## <a id="en_top">What is the locales branch?</a>
- This branch provides translation resources for OSDeveloper.
- *.resx, *.en.resx, and *.ja.resx are original resource that copied from OSDeveloper.
	- These files are just for reference. Please do not make any modifies.
		- If you want to modify, please go to the master branch.
	- *.resx are default resources. This is used when the language was not specified, or translation was not found.
	- *.en.resx are English translation resouces.
	- *.ja.resx are Japanese translation resouces.
- If you make/modify English (the United States of America) translation resources, then please make/modify in *.en-US.resx.
- If you make/modify Japanese (Japan) translation resources, then please make/modify in *.ja-JP.resx.

## Notice
- You should make language resources in resx files of managed projects such as C#.
- You cannot make language resources in native projects such as C/C++.
- If you made/modified language resources, then please add your name and URL of your GitHub account page at CREDITS section on the below.
	- DO NOT REMOVE OTHER'S NAMES!


[言語/Language](#lang)

<!-- CREDITS -->
## <a id="credits">CREDITS</a>
|Language|Native Name|Authours|
|:--|:--|:--|
|en|English|from [master](https://github.com/Takym/OSDeveloper/tree/master)|
|en-US|English (the United States of America)|PLACE HOLDER|
|ja|日本語|from [master](https://github.com/Takym/OSDeveloper/tree/master)|
|ja-JP|日本語 (日本)|@[Takym](https://github.com/Takym/)|
