import createBrowserHistory from 'history/createBrowserHistory'
const history = createBrowserHistory()
history.listen(() => {
    typeof window !== 'undefined' && window.scrollTo(0, 0)
})
export default history
