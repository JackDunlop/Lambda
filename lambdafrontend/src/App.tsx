import './App.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import HomePage from '../src/Pages/HomePage'

function App() {
  return (
      <BrowserRouter>
          <div className="App">
              <Routes>
                  <Route path="/" element={<HomePage/>}/>
              </Routes>
          </div>
      </BrowserRouter>
  )
}

export default App
