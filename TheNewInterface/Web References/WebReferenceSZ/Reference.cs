﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.17929 版自动生成。
// 
#pragma warning disable 1591

namespace TheNewInterface.WebReferenceSZ {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="CXDNBXXServerServiceSoapBinding", Namespace="http://mk.gd.soa.csg.cn")]
    public partial class CXDNBXXServerService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback I_DNJLSBSNJD_CXDNBXXOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public CXDNBXXServerService() {
            this.Url = global::TheNewInterface.Properties.Settings.Default.TheNewInterface_WebReferenceSZ_CXDNBXXServerService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event I_DNJLSBSNJD_CXDNBXXCompletedEventHandler I_DNJLSBSNJD_CXDNBXXCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("I_DNJLSBSNJD_CXDNBXX", RequestElementName="I_DNJLSBSNJD_CXDNBXXRequest", RequestNamespace="http://mk.gd.soa.csg.cn", ResponseNamespace="http://mk.gd.soa.csg.cn", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("replyCode")]
        public string I_DNJLSBSNJD_CXDNBXX(SBJLZCInType SB_JLZC_IN, out SBJLZCOutType SB_JLZC_OUT) {
            object[] results = this.Invoke("I_DNJLSBSNJD_CXDNBXX", new object[] {
                        SB_JLZC_IN});
            SB_JLZC_OUT = ((SBJLZCOutType)(results[1]));
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void I_DNJLSBSNJD_CXDNBXXAsync(SBJLZCInType SB_JLZC_IN) {
            this.I_DNJLSBSNJD_CXDNBXXAsync(SB_JLZC_IN, null);
        }
        
        /// <remarks/>
        public void I_DNJLSBSNJD_CXDNBXXAsync(SBJLZCInType SB_JLZC_IN, object userState) {
            if ((this.I_DNJLSBSNJD_CXDNBXXOperationCompleted == null)) {
                this.I_DNJLSBSNJD_CXDNBXXOperationCompleted = new System.Threading.SendOrPostCallback(this.OnI_DNJLSBSNJD_CXDNBXXOperationCompleted);
            }
            this.InvokeAsync("I_DNJLSBSNJD_CXDNBXX", new object[] {
                        SB_JLZC_IN}, this.I_DNJLSBSNJD_CXDNBXXOperationCompleted, userState);
        }
        
        private void OnI_DNJLSBSNJD_CXDNBXXOperationCompleted(object arg) {
            if ((this.I_DNJLSBSNJD_CXDNBXXCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.I_DNJLSBSNJD_CXDNBXXCompleted(this, new I_DNJLSBSNJD_CXDNBXXCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mk.gd.soa.csg.cn")]
    public partial class SBJLZCInType {
        
        private string zCBHField;
        
        private string dQBMField;
        
        /// <remarks/>
        public string ZCBH {
            get {
                return this.zCBHField;
            }
            set {
                this.zCBHField = value;
            }
        }
        
        /// <remarks/>
        public string DQBM {
            get {
                return this.dQBMField;
            }
            set {
                this.dQBMField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mk.gd.soa.csg.cn")]
    public partial class SBJLZCOutType {
        
        private string sBBSField;
        
        private string zCBHField;
        
        private string sBTMHField;
        
        private string cCBHField;
        
        private string eDDYDMField;
        
        private string bDDLDMField;
        
        private string zQDDJDMField;
        
        private string bMWSDMField;
        
        private string fSWSDMField;
        
        private string xXDMField;
        
        private string eDPLDMField;
        
        private string zSSLXDMField;
        
        private string jLFXDMField;
        
        private string yGCSDMField;
        
        private string wGCSDMField;
        
        private string dNBYLDMField;
        
        private string dNBLXDMField;
        
        private string zSBLField;
        
        private string gZBSField;
        
        private string zZBZDMField;
        
        private string sYJSBZField;
        
        private string zNBZField;
        
        private string fSBZField;
        
        private string lZBZField;
        
        private string yFFLXDMField;
        
        private string jKBBZField;
        
        private string dJBZField;
        
        private string jRFSDMField;
        
        private string sYSMField;
        
        private string tXJKFSDMField;
        
        private string dNBBTLDMField;
        
        private string tXXYDMField;
        
        private string sCCJBSField;
        
        private string sBLBDMField;
        
        private string cSBSField;
        
        private string dHPCHField;
        
        private string sBZTDMField;
        
        private string gDDWBSField;
        
        private string cCRQField;
        
        private string sCTYRQField;
        
        private string cQDWBSField;
        
        private string cQGSDMField;
        
        private string zJJDRQField;
        
        private string sBDJField;
        
        private string tXDZField;
        
        private string tXMKLXDMField;
        
        private string wZPLBMField;
        
        private string gDZCHField;
        
        private string gDZCBZField;
        
        private string fYXZDMField;
        
        /// <remarks/>
        public string SBBS {
            get {
                return this.sBBSField;
            }
            set {
                this.sBBSField = value;
            }
        }
        
        /// <remarks/>
        public string ZCBH {
            get {
                return this.zCBHField;
            }
            set {
                this.zCBHField = value;
            }
        }
        
        /// <remarks/>
        public string SBTMH {
            get {
                return this.sBTMHField;
            }
            set {
                this.sBTMHField = value;
            }
        }
        
        /// <remarks/>
        public string CCBH {
            get {
                return this.cCBHField;
            }
            set {
                this.cCBHField = value;
            }
        }
        
        /// <remarks/>
        public string EDDYDM {
            get {
                return this.eDDYDMField;
            }
            set {
                this.eDDYDMField = value;
            }
        }
        
        /// <remarks/>
        public string BDDLDM {
            get {
                return this.bDDLDMField;
            }
            set {
                this.bDDLDMField = value;
            }
        }
        
        /// <remarks/>
        public string ZQDDJDM {
            get {
                return this.zQDDJDMField;
            }
            set {
                this.zQDDJDMField = value;
            }
        }
        
        /// <remarks/>
        public string BMWSDM {
            get {
                return this.bMWSDMField;
            }
            set {
                this.bMWSDMField = value;
            }
        }
        
        /// <remarks/>
        public string FSWSDM {
            get {
                return this.fSWSDMField;
            }
            set {
                this.fSWSDMField = value;
            }
        }
        
        /// <remarks/>
        public string XXDM {
            get {
                return this.xXDMField;
            }
            set {
                this.xXDMField = value;
            }
        }
        
        /// <remarks/>
        public string EDPLDM {
            get {
                return this.eDPLDMField;
            }
            set {
                this.eDPLDMField = value;
            }
        }
        
        /// <remarks/>
        public string ZSSLXDM {
            get {
                return this.zSSLXDMField;
            }
            set {
                this.zSSLXDMField = value;
            }
        }
        
        /// <remarks/>
        public string JLFXDM {
            get {
                return this.jLFXDMField;
            }
            set {
                this.jLFXDMField = value;
            }
        }
        
        /// <remarks/>
        public string YGCSDM {
            get {
                return this.yGCSDMField;
            }
            set {
                this.yGCSDMField = value;
            }
        }
        
        /// <remarks/>
        public string WGCSDM {
            get {
                return this.wGCSDMField;
            }
            set {
                this.wGCSDMField = value;
            }
        }
        
        /// <remarks/>
        public string DNBYLDM {
            get {
                return this.dNBYLDMField;
            }
            set {
                this.dNBYLDMField = value;
            }
        }
        
        /// <remarks/>
        public string DNBLXDM {
            get {
                return this.dNBLXDMField;
            }
            set {
                this.dNBLXDMField = value;
            }
        }
        
        /// <remarks/>
        public string ZSBL {
            get {
                return this.zSBLField;
            }
            set {
                this.zSBLField = value;
            }
        }
        
        /// <remarks/>
        public string GZBS {
            get {
                return this.gZBSField;
            }
            set {
                this.gZBSField = value;
            }
        }
        
        /// <remarks/>
        public string ZZBZDM {
            get {
                return this.zZBZDMField;
            }
            set {
                this.zZBZDMField = value;
            }
        }
        
        /// <remarks/>
        public string SYJSBZ {
            get {
                return this.sYJSBZField;
            }
            set {
                this.sYJSBZField = value;
            }
        }
        
        /// <remarks/>
        public string ZNBZ {
            get {
                return this.zNBZField;
            }
            set {
                this.zNBZField = value;
            }
        }
        
        /// <remarks/>
        public string FSBZ {
            get {
                return this.fSBZField;
            }
            set {
                this.fSBZField = value;
            }
        }
        
        /// <remarks/>
        public string LZBZ {
            get {
                return this.lZBZField;
            }
            set {
                this.lZBZField = value;
            }
        }
        
        /// <remarks/>
        public string YFFLXDM {
            get {
                return this.yFFLXDMField;
            }
            set {
                this.yFFLXDMField = value;
            }
        }
        
        /// <remarks/>
        public string JKBBZ {
            get {
                return this.jKBBZField;
            }
            set {
                this.jKBBZField = value;
            }
        }
        
        /// <remarks/>
        public string DJBZ {
            get {
                return this.dJBZField;
            }
            set {
                this.dJBZField = value;
            }
        }
        
        /// <remarks/>
        public string JRFSDM {
            get {
                return this.jRFSDMField;
            }
            set {
                this.jRFSDMField = value;
            }
        }
        
        /// <remarks/>
        public string SYSM {
            get {
                return this.sYSMField;
            }
            set {
                this.sYSMField = value;
            }
        }
        
        /// <remarks/>
        public string TXJKFSDM {
            get {
                return this.tXJKFSDMField;
            }
            set {
                this.tXJKFSDMField = value;
            }
        }
        
        /// <remarks/>
        public string DNBBTLDM {
            get {
                return this.dNBBTLDMField;
            }
            set {
                this.dNBBTLDMField = value;
            }
        }
        
        /// <remarks/>
        public string TXXYDM {
            get {
                return this.tXXYDMField;
            }
            set {
                this.tXXYDMField = value;
            }
        }
        
        /// <remarks/>
        public string SCCJBS {
            get {
                return this.sCCJBSField;
            }
            set {
                this.sCCJBSField = value;
            }
        }
        
        /// <remarks/>
        public string SBLBDM {
            get {
                return this.sBLBDMField;
            }
            set {
                this.sBLBDMField = value;
            }
        }
        
        /// <remarks/>
        public string CSBS {
            get {
                return this.cSBSField;
            }
            set {
                this.cSBSField = value;
            }
        }
        
        /// <remarks/>
        public string DHPCH {
            get {
                return this.dHPCHField;
            }
            set {
                this.dHPCHField = value;
            }
        }
        
        /// <remarks/>
        public string SBZTDM {
            get {
                return this.sBZTDMField;
            }
            set {
                this.sBZTDMField = value;
            }
        }
        
        /// <remarks/>
        public string GDDWBS {
            get {
                return this.gDDWBSField;
            }
            set {
                this.gDDWBSField = value;
            }
        }
        
        /// <remarks/>
        public string CCRQ {
            get {
                return this.cCRQField;
            }
            set {
                this.cCRQField = value;
            }
        }
        
        /// <remarks/>
        public string SCTYRQ {
            get {
                return this.sCTYRQField;
            }
            set {
                this.sCTYRQField = value;
            }
        }
        
        /// <remarks/>
        public string CQDWBS {
            get {
                return this.cQDWBSField;
            }
            set {
                this.cQDWBSField = value;
            }
        }
        
        /// <remarks/>
        public string CQGSDM {
            get {
                return this.cQGSDMField;
            }
            set {
                this.cQGSDMField = value;
            }
        }
        
        /// <remarks/>
        public string ZJJDRQ {
            get {
                return this.zJJDRQField;
            }
            set {
                this.zJJDRQField = value;
            }
        }
        
        /// <remarks/>
        public string SBDJ {
            get {
                return this.sBDJField;
            }
            set {
                this.sBDJField = value;
            }
        }
        
        /// <remarks/>
        public string TXDZ {
            get {
                return this.tXDZField;
            }
            set {
                this.tXDZField = value;
            }
        }
        
        /// <remarks/>
        public string TXMKLXDM {
            get {
                return this.tXMKLXDMField;
            }
            set {
                this.tXMKLXDMField = value;
            }
        }
        
        /// <remarks/>
        public string WZPLBM {
            get {
                return this.wZPLBMField;
            }
            set {
                this.wZPLBMField = value;
            }
        }
        
        /// <remarks/>
        public string GDZCH {
            get {
                return this.gDZCHField;
            }
            set {
                this.gDZCHField = value;
            }
        }
        
        /// <remarks/>
        public string GDZCBZ {
            get {
                return this.gDZCBZField;
            }
            set {
                this.gDZCBZField = value;
            }
        }
        
        /// <remarks/>
        public string FYXZDM {
            get {
                return this.fYXZDMField;
            }
            set {
                this.fYXZDMField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void I_DNJLSBSNJD_CXDNBXXCompletedEventHandler(object sender, I_DNJLSBSNJD_CXDNBXXCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class I_DNJLSBSNJD_CXDNBXXCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal I_DNJLSBSNJD_CXDNBXXCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public SBJLZCOutType SB_JLZC_OUT {
            get {
                this.RaiseExceptionIfNecessary();
                return ((SBJLZCOutType)(this.results[1]));
            }
        }
    }
}

#pragma warning restore 1591