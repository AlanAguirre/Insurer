import React from "react"
import RefreshIndicator from 'material-ui/RefreshIndicator'

export default class Loading extends React.Component {
    
    
  render() {
    return <div style={{position: 'absolute', zIndex:10, width: '100%', height: '100%', backgroundColor:'rgba(255,255,255, 0.5)'}}>
                  <RefreshIndicator
                    size={40}
                    left={10}
                    top={0}
                    status="loading"
                    style={{transform:'translateX(-50%) translateY(-50%)', top: '50%', left:'50%'}}
                  />
                </div>;
  }
}