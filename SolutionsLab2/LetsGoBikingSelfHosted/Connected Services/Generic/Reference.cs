﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LetsGoBikingSelfHosted.Generic {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Generic.IProxyCache")]
    public interface IProxyCache {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxyCache/getContractsList", ReplyAction="http://tempuri.org/IProxyCache/getContractsListResponse")]
        GenericProxyCache.JCDecauxItem getContractsList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxyCache/getContractsList", ReplyAction="http://tempuri.org/IProxyCache/getContractsListResponse")]
        System.Threading.Tasks.Task<GenericProxyCache.JCDecauxItem> getContractsListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxyCache/getStationsList", ReplyAction="http://tempuri.org/IProxyCache/getStationsListResponse")]
        GenericProxyCache.JCDecauxItem getStationsList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxyCache/getStationsList", ReplyAction="http://tempuri.org/IProxyCache/getStationsListResponse")]
        System.Threading.Tasks.Task<GenericProxyCache.JCDecauxItem> getStationsListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxyCache/getStationsListWithContractName", ReplyAction="http://tempuri.org/IProxyCache/getStationsListWithContractNameResponse")]
        GenericProxyCache.JCDecauxItem getStationsListWithContractName(string contractName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxyCache/getStationsListWithContractName", ReplyAction="http://tempuri.org/IProxyCache/getStationsListWithContractNameResponse")]
        System.Threading.Tasks.Task<GenericProxyCache.JCDecauxItem> getStationsListWithContractNameAsync(string contractName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProxyCacheChannel : LetsGoBikingSelfHosted.Generic.IProxyCache, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProxyCacheClient : System.ServiceModel.ClientBase<LetsGoBikingSelfHosted.Generic.IProxyCache>, LetsGoBikingSelfHosted.Generic.IProxyCache {
        
        public ProxyCacheClient() {
        }
        
        public ProxyCacheClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProxyCacheClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProxyCacheClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProxyCacheClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public GenericProxyCache.JCDecauxItem getContractsList() {
            return base.Channel.getContractsList();
        }
        
        public System.Threading.Tasks.Task<GenericProxyCache.JCDecauxItem> getContractsListAsync() {
            return base.Channel.getContractsListAsync();
        }
        
        public GenericProxyCache.JCDecauxItem getStationsList() {
            return base.Channel.getStationsList();
        }
        
        public System.Threading.Tasks.Task<GenericProxyCache.JCDecauxItem> getStationsListAsync() {
            return base.Channel.getStationsListAsync();
        }
        
        public GenericProxyCache.JCDecauxItem getStationsListWithContractName(string contractName) {
            return base.Channel.getStationsListWithContractName(contractName);
        }
        
        public System.Threading.Tasks.Task<GenericProxyCache.JCDecauxItem> getStationsListWithContractNameAsync(string contractName) {
            return base.Channel.getStationsListWithContractNameAsync(contractName);
        }
    }
}