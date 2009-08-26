#ifndef cp866cvtH
#define cp866cvtH

#include <locale>
#include <windows.h>
#include <algorithm>

class rus_codecvt : public std::codecvt<char, char, std::mbstate_t> {
protected:
  virtual result do_in(std::mbstate_t&,
      const char* from, const char* from_end, const char*& from_next,
      char* to, char* to_limit, char*& to_next
  ) const {
    const int i = std::min(to_limit - to, from_end - from);
    ::OemToCharBuff(from, to, i);
    from_next = from + i;
    to_next = to + i;
    return ok;
  }
  virtual result do_out(std::mbstate_t&,
    const char* from, const char* from_end, const char*& from_next,
    char* to, char* to_limit, char*& to_next
  ) const {
    const int i = std::min(to_limit-to, from_end-from);
    ::CharToOemBuff(from, to, i);
    from_next = from + i;
    to_next = to + i;
    return ok;
  }
  virtual bool do_always_noconv() const throw() {return false;}
  virtual int do_encoding() const throw() {return 1;}
  virtual int do_length (
    const std::mbstate_t&, const char* from, const char* end, size_t max
  ) const {
    return std::min(static_cast<size_t>(end - from), max);
  }
  virtual int do_max_length() const throw() {return INT_MAX;}
};

#pragma comment(lib, "User32.lib")
#endif 