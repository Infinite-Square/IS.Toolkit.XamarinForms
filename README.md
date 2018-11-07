# IS.XF.Toolkit

## Expander
### Bindable properties :
- BackgroundColor (`Color`)
- CornerRadius (`double`)
- BorderColor (`Color`)
- BorderWidth (`double`)
- LeftIcon (`ImageSource`)
- RightIcon (`ImageSource`)
- HeaderText (`string`)
- HeaderTextColor (`Color`)
- HeaderFontFamily (`string`)
- HeaderFontAttributes (`FontAttributes`)
- Content (`View`)
- IsOpen (`bool`)
- Animated (`bool`)
- SeparatorColor (`Color`)
- SeparatorHeight (`double`)
- HasSeparator (`bool`)

## Picker
This picker customize and simplify picker appearence.
ItemsSource is a List of `AvailableValue` which contains a Label (string of your object you wan't to display) and a Value (which is your object).

### Bindable properties
- Title (`string`) : Label to display above the picker. Can be null.
- TitleTextColor (`Color`) : Title's color
- TitleFontFamily (`string`) : Title's font family
- TitleFontSize (`double`) : Title's font size
- TitleFontAttributes (`FontAttributes`) : Title's font attributes
___
- SelectedItem (`AvailableValue`) : Selected value (label and value)
- TextColor (`Color`) : Displayed selected value text color
- FontSize (`double`) : Displayed selected value font size
- FontFamily (`string`) : Displayed selected value font family
- FontAttributes (`FontAttributes`) : Displayed selected value font attributes
___
- Placeholder (`string`) : Placeholder. If null, blank
- PlaceholderTextColor (`Color`) : Placeholder text color
- PlaceholderFontSize (`double`) : Placeholder font size
- PlaceholderFontFamily (`string`) : Placeholder font family
- PlaceholderFontAttributes (`FontAttributes`) : Placeholder font attributes
___
- BorderColor (`Color`) : Border's color of the picker
- CornerRadius (`double`) : Border's radius of the picker
- BackgroundColor (`Color`) : Color of the picker
- IconSource (`ImageSource`) : Right icon's source
- IsClearable (`bool`) : Indicate if the picker can be cleared. If can be cleared, show a clear icon

### Event   
- SelectedItemChanged (`EventHandler<object>`) : Fired when the selected item is changed (or cleared)