using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Makaretu.Dns;

namespace Advertiser;

public partial class MainViewModel 
    : ObservableObject
{
    private ServiceProfile _serviceProfile;
    private ServiceDiscovery _serviceDiscovery;
    
    private bool _isRunning = false;
    
    [ObservableProperty] 
    private ObservableCollection<string> _itemData;

    public MainViewModel()
    {
        string serviceRecordName = "_cbliteptp._tcp";
        string deviceId = AppPreferencesService.GetUniqueDeviceId();
        _serviceProfile = new ServiceProfile(deviceId, serviceRecordName, 5559);
        _serviceDiscovery = new ServiceDiscovery();
        
        ChangeStatusCommand = new RelayCommand(ChangeStatus);
        ItemData = new(); 
        ItemData.Add($"DeviceId: {deviceId}");
        ItemData.Add($"Service Name: {serviceRecordName}");
    }

    public ICommand ChangeStatusCommand { get; }
    
    private void ChangeStatus()
    {
        _isRunning = !_isRunning;
        ItemData.Add($"Service Running change to {_isRunning}");
        
        if (_isRunning)
        {
            _serviceDiscovery.Advertise(_serviceProfile);
        }
        else
        {
            _serviceDiscovery.Unadvertise(_serviceProfile);
        }
    }
    
    
}