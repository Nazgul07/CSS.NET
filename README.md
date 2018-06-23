# CSS.NET
A library for applying CSS styles to Windows Desktop applications (Winforms, WPF)

Style your Windows Desktop applications using CSS.

### Using Name selectors:
```csharp
public Form1()
{
  InitializeComponent();
  this.ApplyFormCss(@"
  #button1 {
    Height:50;
    Width:200;
    Text: ""An awesome button"";
  }
  #label1 {
    ForeColor: Color.Red;
    Text: ""An awesome label"";
    Location: new Point(10, 10);
  }
  ");
}
```

### Using Tag (Control Type) selectors:
```csharp
public Form1()
{
  InitializeComponent();
  this.ApplyFormCss(@"
  Button {
    Height:50;
    Width:200;
    Text: ""An awesome button"";
  }
  Label {
    ForeColor: Color.Red;
    Text: ""An awesome label"";
    Location: new Point(10, 10);
  }
  ");
}
```

### Using nested selectors:
```csharp
public Form1()
{
  InitializeComponent();
  this.ApplyFormCss(@"
  #panel1 > #button1 {
    Height:50;
    Width:200;
    Text: ""An awesome button"";
  }
  Panel Label {
    ForeColor: Color.Red;
    Text: ""An awesome label"";
    Location: new Point(10, 10);
  }
  ");
}
```
