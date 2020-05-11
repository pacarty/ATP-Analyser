import React from 'react';

import { PlayerSelector } from './components';

class App extends React.Component {
    render() {
        return (
            <div>
                <h2>ATP rankings analyser</h2>
                <PlayerSelector />
            </div>
        );
    }
}

export default App;