package com.agile.security;

import com.agile.dto.AccountDto;
import com.agile.entity.Account;
import com.agile.repository.AccountRepository;
import lombok.RequiredArgsConstructor;
import lombok.extern.log4j.Log4j2;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import java.util.stream.Collectors;

@RequiredArgsConstructor
@Service
@Log4j2
public class CustomUserDetailsService implements UserDetailsService {

    private final AccountRepository accountRepository;

    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {

        log.info("-------loadUserByUsername-------" + username);

        Account account = accountRepository.getWithRolesFromEmail(username);

        if (account == null) {
            throw new UsernameNotFoundException(username + " not found");
        }

        AccountDto accountDto = new AccountDto(
                String.valueOf(accountRepository.getWithRolesFromDbId(account.getAccountDbId()).getAccountDbId()),
                account.getPassword(),
                account.isSocial(),
                account.getAccountRoleList()
                        .stream()
                        .map(accountRole -> accountRole.name()).collect(Collectors.toList())
        );

        log.info(accountDto);

        return accountDto;
    }
}
