import "./App.css";
import { BrowserRouter as Router } from "react-router-dom";

import BaseRoutes from "routes";

function App() {
    return (
        <Router>
            <BaseRoutes />
        </Router>
    );
}

export default App;
